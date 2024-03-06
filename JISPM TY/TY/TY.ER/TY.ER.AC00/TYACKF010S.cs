using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.AC00
{
    /// <summary>
    /// 자금실적 집계 조회 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2012.09.27 18:57
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_29R7L335 : 자금실적 수입 조회
    ///  TY_P_AC_29R7P336 : 자금실적 지출 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_29R7R337 : 자금실적 수입 집계 조회
    ///  TY_S_AC_29R7T338 : 자금실적 지출 집계 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  GEDDATE : 종료일자
    ///  GSTDATE : 시작일자
    /// </summary>
    public partial class TYACKF010S : TYBase
    {
        #region Description : 폼 로드 이벤트
        public TYACKF010S()
        {
            InitializeComponent();
        }

        private void TYACKF010S_Load(object sender, System.EventArgs e)
        {
            this.DTP01_GSTDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));
            this.DTP01_GEDDATE.SetValue(DateTime.Now.AddMonths(1).ToString("yyyy-MM-dd"));

        }
        #endregion

        #region Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            //수입
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_29R7L335", this.DTP01_GSTDATE.GetString(), this.DTP01_GEDDATE.GetString(),
                                                        this.DTP01_GSTDATE.GetString(), this.DTP01_GEDDATE.GetString(),
                                                        this.DTP01_GSTDATE.GetString(), this.DTP01_GEDDATE.GetString());
            DataSet ds1 = this.DbConnector.ExecuteDataSet();

            this.FPS91_TY_S_AC_29R7R337.SetValue(UP_ComputeDs_In(ds1));
            //지출
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_29R7P336", this.DTP01_GSTDATE.GetString(), this.DTP01_GEDDATE.GetString(),
                                                        this.DTP01_GSTDATE.GetString(), this.DTP01_GEDDATE.GetString(),
                                                        this.DTP01_GSTDATE.GetString(), this.DTP01_GEDDATE.GetString(),
                                                        this.DTP01_GSTDATE.GetString(), this.DTP01_GEDDATE.GetString());
            DataSet ds2 = this.DbConnector.ExecuteDataSet();

            this.FPS91_TY_S_AC_29R7T338.SetValue(UP_ComputeDs_Out(ds2));


        }
        #endregion

        #region Description : 집계 수입
        public DataSet UP_ComputeDs_In(DataSet dsindex_In)
        {
            DataSet dsParam = new DataSet();

            string sFilter = "";

            try
            {
                //수입
                DataTable FundSumTable = new DataTable();
                DataRow FundSumRow;

                FundSumTable.Columns.Add("FundSumCode", typeof(System.String));
                FundSumTable.Columns.Add("FundSumAmt", typeof(System.Double));

                for (int i = 0; i < dsindex_In.Tables[0].Rows.Count; i++)
                {

                    //현금수금,받을어음
                    if (dsindex_In.Tables[0].Rows[i]["PCCDFD"].ToString() == "11100" ||
                        dsindex_In.Tables[0].Rows[i]["PCCDFD"].ToString() == "11200")
                    {
                        FundSumRow = FundSumTable.NewRow();
                        FundSumRow["FundSumCode"] = UP_ConvertDPAC(dsindex_In.Tables[0].Rows[i]["PCDPAC"].ToString(),
                                                                    dsindex_In.Tables[0].Rows[i]["A3ABFD"].ToString());
                        FundSumRow["FundSumAmt"] = Convert.ToDouble(dsindex_In.Tables[0].Rows[i][4].ToString());
                        FundSumTable.Rows.Add(FundSumRow);
                    }
                    else
                    {
                        FundSumRow = FundSumTable.NewRow();
                        FundSumRow["FundSumCode"] = dsindex_In.Tables[0].Rows[i]["A3ABFD"].ToString();
                        FundSumRow["FundSumAmt"] = Convert.ToDouble(dsindex_In.Tables[0].Rows[i][4].ToString());
                        FundSumTable.Rows.Add(FundSumRow);
                    }

                }

                //일자별 합계 ADD
                int iNum = 0;

                iNum = FundSumTable.Rows.Count;

                sFilter = "";

                if (iNum > 0)
                {
                    FundSumRow = FundSumTable.NewRow();
                    FundSumTable.Rows.InsertAt(FundSumRow, iNum);

                    FundSumTable.Rows[iNum]["FundSumCode"] = "합 계";
                    sFilter = "";
                    FundSumTable.Rows[iNum]["FundSumAmt"] = Convert.ToDouble(Get_Numeric(FundSumTable.Compute("SUM(FundSumAmt)", sFilter).ToString()));

                }

                FundSumTable.TableName = "AC5031";

                dsParam.Tables.Add(FundSumTable);

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

        #region Description : 집계 지출
        public DataSet UP_ComputeDs_Out(DataSet dsindex_In)
        {
            DataSet dsParam = new DataSet();

            string sFilter = "";
            string sSort = "";

            string sPCCDFD = "";
            string sPCDPAC = "";
            double d자금실적 = 0;
            double d미지급금 = 0;
            double d외상매입금 = 0;
            double d지급어음 = 0;
            double d기타미지급금 = 0;

            try
            {
                //지출
                DataTable FundSumTable = new DataTable();
                DataRow FundSumRow;

                FundSumTable.Columns.Add("FundSumCode", typeof(System.String)); //자금항목
                FundSumTable.Columns.Add("FundSumSJAmt", typeof(System.Double));  //실적금액
                FundSumTable.Columns.Add("FundSumAmt1", typeof(System.Double));  //미지급금
                FundSumTable.Columns.Add("FundSumAmt2", typeof(System.Double));  //외상매입금
                FundSumTable.Columns.Add("FundSumAmt3", typeof(System.Double));  //지급어음
                FundSumTable.Columns.Add("FundSumAmt4", typeof(System.Double));  //기타미지급금
                FundSumTable.Columns.Add("FundSumHapAmt", typeof(System.Double));  //합계

                //자금 항목 코드 기준으로 정렬
                sFilter = "";
                sSort = " PCCDFD ASC ";
                DataRow[] DataRowSort;
                DataRowSort = dsindex_In.Tables[0].Select(sFilter, sSort);

                for (int i = 0; i < DataRowSort.Length; i++)
                {
                    

                    if (i > 0)
                    {

                        if (d자금실적 > 0 || d미지급금 > 0 || d외상매입금 > 0 ||
                            d지급어음 > 0 || d기타미지급금 > 0)
                        {

                            if (sPCCDFD != DataRowSort[i]["PCCDFD"].ToString() ||
                                sPCDPAC != DataRowSort[i]["PCDPAC"].ToString())
                            {
                                //usance, 상품구매, 기타무역비용 
                                if (DataRowSort[i - 1]["PCCDFD"].ToString() == "21610" ||
                                    DataRowSort[i - 1]["PCCDFD"].ToString() == "21620" ||
                                    DataRowSort[i - 1]["PCCDFD"].ToString() == "21430")
                                {
                                    FundSumRow = FundSumTable.NewRow();
                                    FundSumRow["FundSumCode"] = UP_ConvertDPAC(DataRowSort[i - 1]["PCDPAC"].ToString(),
                                        DataRowSort[i - 1]["A3ABFD"].ToString());
                                    FundSumRow["FundSumSJAmt"] = d자금실적;
                                    FundSumRow["FundSumAmt1"] = d미지급금;
                                    FundSumRow["FundSumAmt2"] = d외상매입금;
                                    FundSumRow["FundSumAmt3"] = d지급어음;
                                    FundSumRow["FundSumAmt4"] = d기타미지급금;
                                    FundSumRow["FundSumHapAmt"] = d자금실적 + d미지급금 + d외상매입금 + d지급어음 + d기타미지급금;
                                    FundSumTable.Rows.Add(FundSumRow);
                                }
                                else
                                {
                                    FundSumRow = FundSumTable.NewRow();
                                    FundSumRow["FundSumCode"] = DataRowSort[i - 1]["A3ABFD"].ToString();
                                    FundSumRow["FundSumSJAmt"] = d자금실적;
                                    FundSumRow["FundSumAmt1"] = d미지급금;
                                    FundSumRow["FundSumAmt2"] = d외상매입금;
                                    FundSumRow["FundSumAmt3"] = d지급어음;
                                    FundSumRow["FundSumAmt4"] = d기타미지급금;
                                    if (DataRowSort[i - 1]["PCCDFD"].ToString() == "22500" ||
                                        DataRowSort[i - 1]["PCCDFD"].ToString() == "22600" ||
                                        DataRowSort[i - 1]["PCCDFD"].ToString() == "22700" ||
                                        DataRowSort[i - 1]["PCCDFD"].ToString() == "22800")
                                    {
                                        FundSumRow["FundSumHapAmt"] = 0;
                                    }
                                    else
                                    {
                                        FundSumRow["FundSumHapAmt"] = d자금실적 + d미지급금 + d외상매입금 + d지급어음 + d기타미지급금;
                                    }
                                    FundSumTable.Rows.Add(FundSumRow);
                                }
                                d자금실적 = 0;
                                d미지급금 = 0;
                                d외상매입금 = 0;
                                d지급어음 = 0;
                                d기타미지급금 = 0;
                            }
                        }
                    }

                    sPCCDFD = DataRowSort[i]["PCCDFD"].ToString();
                    sPCDPAC = DataRowSort[i]["PCDPAC"].ToString();

                    

                    switch (DataRowSort[i]["GUBN"].ToString())
                    {
                        case "A":
                            d자금실적 = Convert.ToDouble(DataRowSort[i][4].ToString());
                            break;
                        case "1":
                            d미지급금 = Convert.ToDouble(DataRowSort[i][4].ToString());
                            break;
                        case "2":
                            d외상매입금 = Convert.ToDouble(DataRowSort[i][4].ToString());
                            break;
                        case "3":
                            d지급어음 = Convert.ToDouble(DataRowSort[i][4].ToString());
                            break;
                        case "4":
                            d기타미지급금 = Convert.ToDouble(DataRowSort[i][4].ToString());
                            break;
                    }

                    //자료의 마지막
                    if (i == DataRowSort.Length - 1)
                    {
                        if (sPCCDFD != DataRowSort[i - 1]["PCCDFD"].ToString() ||
                            sPCDPAC != DataRowSort[i - 1]["PCDPAC"].ToString())
                        {
                            FundSumRow = FundSumTable.NewRow();
                            FundSumRow["FundSumCode"] = DataRowSort[i]["A3ABFD"].ToString();
                            FundSumRow["FundSumSJAmt"] = d자금실적;
                            FundSumRow["FundSumAmt1"] = d미지급금;
                            FundSumRow["FundSumAmt2"] = d외상매입금;
                            FundSumRow["FundSumAmt3"] = d지급어음;
                            FundSumRow["FundSumAmt4"] = d기타미지급금;
                            FundSumRow["FundSumHapAmt"] = d자금실적 + d미지급금 + d외상매입금 + d지급어음 + d기타미지급금;
                            FundSumTable.Rows.Add(FundSumRow);
                        }

                    }

                }

                //일자별 합계 ADD
                int iNum = 0;

                iNum = FundSumTable.Rows.Count;

                sFilter = "";

                if (iNum > 0)
                {
                    FundSumRow = FundSumTable.NewRow();
                    FundSumTable.Rows.InsertAt(FundSumRow, iNum);

                    FundSumTable.Rows[iNum]["FundSumCode"] = "합 계";
                    sFilter = "";
                    FundSumTable.Rows[iNum]["FundSumSJAmt"] = Convert.ToDouble(Get_Numeric(FundSumTable.Compute("SUM(FundSumSJAmt)", sFilter).ToString()));
                    FundSumTable.Rows[iNum]["FundSumAmt1"] = Convert.ToDouble(Get_Numeric(FundSumTable.Compute("SUM(FundSumAmt1)", sFilter).ToString()));
                    FundSumTable.Rows[iNum]["FundSumAmt2"] = Convert.ToDouble(Get_Numeric(FundSumTable.Compute("SUM(FundSumAmt2)", sFilter).ToString()));
                    FundSumTable.Rows[iNum]["FundSumAmt3"] = Convert.ToDouble(Get_Numeric(FundSumTable.Compute("SUM(FundSumAmt3)", sFilter).ToString()));
                    FundSumTable.Rows[iNum]["FundSumAmt4"] = Convert.ToDouble(Get_Numeric(FundSumTable.Compute("SUM(FundSumAmt4)", sFilter).ToString()));
                    FundSumTable.Rows[iNum]["FundSumHapAmt"] = Convert.ToDouble(Get_Numeric(FundSumTable.Compute("SUM(FundSumHapAmt)", sFilter).ToString()));
                }

                FundSumTable.TableName = "AC5031";

                dsParam.Tables.Add(FundSumTable);

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

        #region Description : 사업부 이름 변경
        private string UP_ConvertDPAC(string sDPAC, string sA3ABFD)
        {
            string sDPName = "";

            switch (sDPAC)
            {
                case "B100":
                    sDPName = "이전자원";
                    break;
                case "B101":
                    sDPName = "사료팀";
                    break;
                case "B102":
                    sDPName = "자원팀";
                    break;
                case "B200":
                    sDPName = "이전화학팀";
                    break;
                case "B201":
                    sDPName = "화학팀";
                    break;
                case "B500":
                    sDPName = "이전철강팀";
                    break;
                case "B501":
                    sDPName = "철강팀";
                    break;
                case "B601":
                    sDPName = "석유팀";
                    break;
                case "B701":
                    sDPName = "개발팀";
                    break;
                case "B801":
                    sDPName = "석유화학";
                    break;
                case "B802":
                    sDPName = "농업자원";
                    break;
                case "B803":
                    sDPName = "철강";
                    break;
                case "S":
                    sDPName = "SILO";
                    break;
                case "T":
                    sDPName = "UTT";
                    break;
                case "A":
                    sDPName = "관리";
                    break;
            }

            return sA3ABFD + " " + sDPName;
        }
        #endregion   
    }
}
