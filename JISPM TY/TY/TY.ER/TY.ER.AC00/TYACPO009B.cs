using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.AC00
{
    /// <summary>
    /// EIS 장기재고현황 생성 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2013.07.15 14:11
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_37F22106 : EIS 장기재고현황 조회
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
    ///  GSTYYMM : 시작년월
    /// </summary>
    public partial class TYACPO009B : TYBase
    {
        #region Description : 폼 로드 이벤트
        public TYACPO009B()
        {
            InitializeComponent();
        }

        private void TYACPO009B_Load(object sender, System.EventArgs e)
        {
            this.BTN61_BATCH.ProcessCheck += new TButton.CheckHandler(BTN61_BATCH_ProcessCheck);

            this.DTP01_GSTYYMM.SetValue(DateTime.Now.ToString("yyyy-MM"));

            this.SetStartingFocus(this.DTP01_GSTYYMM);
        }
        #endregion

        #region  Description : 생성 버튼 이벤트
        private void BTN61_BATCH_Click(object sender, EventArgs e)
        {
            double dStockPriceGap = 0;
            double dStockPrice = 0;

            string sPO_NO_OLD = "";
            string sBL_NO_OLD = "";
            string sITEMCODE_OLD = "";

            System.Collections.Generic.List<object[]> datas = new System.Collections.Generic.List<object[]>();

            //삭제
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_37F3J108", this.DTP01_GSTYYMM.GetString().ToString().Substring(0, 6));
            this.DbConnector.ExecuteTranQuery();


            DateTime dt = Convert.ToDateTime(this.DTP01_GSTYYMM.GetString().ToString().Substring(0, 4) + "-" + this.DTP01_GSTYYMM.GetString().ToString().Substring(4,2)+"-"+"01");

            dt = dt.AddMonths(1);
            dt = dt.AddDays(-1);

            string sKIJUNDATE = Convert.ToString(dt.Year) + Set_Fill2(dt.Month.ToString()) + Set_Fill2(dt.Day.ToString());   

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_37F22106", sKIJUNDATE, sKIJUNDATE, sKIJUNDATE,sKIJUNDATE, this.DTP01_GSTYYMM.GetString().Substring(0, 6), this.DTP01_GSTYYMM.GetString().Substring(0, 6));
            DataTable dr = this.DbConnector.ExecuteDataTable();

            if (dr.Rows.Count > 0)
            {
                for (int i = 0; i < dr.Rows.Count; i++)
                {
                    dStockPriceGap = 0;

                    if (Convert.ToDouble(dr.Rows[i]["groupjegoqty"].ToString()) >= Convert.ToDouble(dr.Rows[i]["jgsjgqty"].ToString()))
                    {
                        if (Convert.ToDouble(dr.Rows[i]["jgisjamt"].ToString()) > Convert.ToDouble(dr.Rows[i]["groupstockprice"].ToString()))
                        {
                            dStockPriceGap = Convert.ToDouble(dr.Rows[i]["jgisjamt"].ToString()) - Convert.ToDouble(dr.Rows[i]["groupstockprice"].ToString());
                        }
                        else
                        {
                            dStockPriceGap = Convert.ToDouble(dr.Rows[i]["groupstockprice"].ToString()) - Convert.ToDouble(dr.Rows[i]["jgisjamt"].ToString());
                        }
                    }

                    if (i != 0 && 
                        (dr.Rows[i]["PO_NO"].ToString() != sPO_NO_OLD || dr.Rows[i]["HOUSE_BL_NO"].ToString() != sBL_NO_OLD || dr.Rows[i]["ITEM_CODE"].ToString() != sITEMCODE_OLD)
                       )
                    {
                        dStockPrice = Convert.ToDouble(dr.Rows[i]["STOCKPRICE"].ToString()) - dStockPriceGap;
                    }
                    else
                    {
                        dStockPrice = Convert.ToDouble(dr.Rows[i]["STOCKPRICE"].ToString());
                    }

                    if (i == (dr.Rows.Count - 1))
                    {
                        dStockPrice = Convert.ToDouble(dr.Rows[i]["STOCKPRICE"].ToString()) - dStockPriceGap;
                    }

                    dStockPrice = Math.Floor(dStockPrice / 1000000) * 1000000;

                    datas.Add(new object[] { this.DTP01_GSTYYMM.GetString().Substring(0, 6),
                                             dr.Rows[i]["PO_NO"].ToString().Substring(4,6),
                                             dr.Rows[i]["SN"].ToString(),
                                             dr.Rows[i]["PO_NO"].ToString(),
                                             dr.Rows[i]["HOUSE_BL_NO"].ToString(),
                                             dr.Rows[i]["ITEM_CODE"].ToString(),
                                             dr.Rows[i]["RECEIPT_DATE"].ToString(),
                                             dr.Rows[i]["JEGOQTY"].ToString(),
                                             dStockPrice.ToString(),
                                             TYUserInfo.EmpNo});

                    sPO_NO_OLD = dr.Rows[i]["PO_NO"].ToString();
                    sBL_NO_OLD = dr.Rows[i]["HOUSE_BL_NO"].ToString();
                    sITEMCODE_OLD = dr.Rows[i]["ITEM_CODE"].ToString();
                }
            }

            if (datas.Count > 0)
            {
                this.DbConnector.CommandClear();
                foreach (object[] data in datas)
                {
                    this.DbConnector.Attach("TY_P_AC_37F44109", data);
                }

                this.DbConnector.ExecuteTranQueryList();
            }
            
            this.ShowMessage("TY_M_GB_26E30875");
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
                if (dt1.Rows[0]["ECGUBUN"].ToString() == "Y" || dt1.Rows[0]["ECGUBUN"].ToString() == "Z" )
                {
                    this.ShowMessage("TY_M_AC_27H6I063"); // EIS 적용 완료상태 입니다. (처리 불가)
                    e.Successed = false;
                    return;
                }
            }

            //무역마감 체크
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_25G2U498", this.DTP01_GSTYYMM.GetString().ToString().Substring(0, 6));
            Int16 iCnt = Convert.ToInt16(this.DbConnector.ExecuteScalar());

            if (iCnt <= 0)
            {
                this.ShowMessage("TY_M_AC_28AAV362");
                e.Successed = false;
                return;
            }

            if (!this.ShowMessage("TY_M_GB_26E2Z874"))
            {
                e.Successed = false;
                return;
            }
        }
        #endregion

        #region  Description : 조회 버튼 이벤트
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close(); 
        }
        #endregion
    }
}
