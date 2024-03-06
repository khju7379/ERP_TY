using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.UT00
{
    /// <summary>
    /// 통관화주별 재고조회 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2016.06.16 15:15
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_UT_66FD4200 : 대표 거래처 코드 조회
    ///  TY_P_UT_66GF1282 : 통관화주별 재고조회(마스타)
    ///  TY_P_UT_66GF2283 : 통관화주별 재고조회(디테일)
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_UT_66GFA285 : 통관화주별 재고조회(마스타)
    ///  TY_S_UT_66GFB286 : 통관화주별 재고조회(디테일)
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_2422N250 : 자료가 존재하지 않습니다.
    ///  TY_M_GB_2BF7Y364 : 조회가 완료되었습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  CNHWAJU : 화주
    ///  IPHANG : 접안일자
    ///  CJCUQTYTOT : 총 통관량
    ///  CUJEQTYTOT : 총 통관재고
    /// </summary>
    public partial class TYUTIN019S : TYBase
    {
        public TYUTIN019S()
        {
            InitializeComponent();
        }

        private void TYUTIN019S_Load(object sender, System.EventArgs e)
        {
            this.DTP01_IPHANG.SetValue("");
            SetStartingFocus(this.CBH01_CNHWAJU.CodeText);
        }

        #region Description : 조회버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            string sHWAJU = this.CBH01_CNHWAJU.GetValue().ToString();
            string sDATE = this.DTP01_IPHANG.GetValue().ToString();

            // 대표거래처 코드 가져오기
            sHWAJU = Get_VNCODE(this.CBH01_CNHWAJU.GetValue().ToString());

            if (sDATE == "" || sDATE == "19000101")
            {
                sDATE = "";
            }

            DataTable dt = new DataTable();

            this.FPS91_TY_S_UT_66GFA285.Initialize();
            this.FPS91_TY_S_UT_66GFB286.Initialize();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_66GF1282", sHWAJU,
                                                        sDATE);

            dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_UT_66GFA285.SetValue(dt);

            this.TXT01_CJCHQTYTOT.Text = "0.000";
            this.TXT01_CUJEQTYTOT.Text = "0.000";
        }
        #endregion

        #region Description : 마스타 그리드 선택
        private void FPS91_TY_S_UT_66GFA285_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            DataTable dt = new DataTable();

            this.FPS91_TY_S_UT_66GFB286.Initialize();

            this.DbConnector.CommandClear();
            //이전 소스 
            //this.DbConnector.Attach("TY_P_UT_66GF2283", this.FPS91_TY_S_UT_66GFA285.GetValue("CJIPHANG").ToString(),
            //                                            this.FPS91_TY_S_UT_66GFA285.GetValue("BONSUN").ToString(),
            //                                            this.FPS91_TY_S_UT_66GFA285.GetValue("CJHWAJU").ToString(),
            //                                            this.FPS91_TY_S_UT_66GFA285.GetValue("HWAMUL").ToString(),
            //                                            this.FPS91_TY_S_UT_66GFA285.GetValue("BLNO").ToString(),
            //                                            this.FPS91_TY_S_UT_66GFA285.GetValue("MSNSEQ").ToString(),
            //                                            this.FPS91_TY_S_UT_66GFA285.GetValue("HSNSEQ").ToString(),
            //                                            this.CBH01_CNHWAJU.GetValue().ToString()
            //                                            );

            this.DbConnector.Attach("TY_P_UT_936AX003", this.FPS91_TY_S_UT_66GFA285.GetValue("CJIPHANG").ToString(),
                                                        this.FPS91_TY_S_UT_66GFA285.GetValue("BONSUN").ToString(),
                                                        this.FPS91_TY_S_UT_66GFA285.GetValue("CJHWAJU").ToString(),
                                                        this.FPS91_TY_S_UT_66GFA285.GetValue("HWAMUL").ToString(),
                                                        this.FPS91_TY_S_UT_66GFA285.GetValue("BLNO").ToString(),
                                                        this.FPS91_TY_S_UT_66GFA285.GetValue("MSNSEQ").ToString(),
                                                        this.FPS91_TY_S_UT_66GFA285.GetValue("HSNSEQ").ToString(),
                                                        this.CBH01_CNHWAJU.GetValue().ToString(),
                                                        this.FPS91_TY_S_UT_66GFA285.GetValue("CJCUSTIL").ToString(),
                                                        this.FPS91_TY_S_UT_66GFA285.GetValue("CJCHASU").ToString()
                                                        );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.FPS91_TY_S_UT_66GFB286.SetValue(UP_ChangeDatatable(dt));
            }
            else
            {
                this.FPS91_TY_S_UT_66GFB286.SetValue(dt);
            }
        }
        #endregion

        #region Description : 데이터 테이블 변경
        private DataTable UP_ChangeDatatable(DataTable dt)
        {
            DataTable rtnDt = new DataTable();

            string sCHBLNO = this.FPS91_TY_S_UT_66GFA285.GetValue("BLNO").ToString();
            string sCHHWAJUNM = this.FPS91_TY_S_UT_66GFA285.GetValue("CJHWAJUNM").ToString();

            double dCJCUQTY = Convert.ToDouble(this.FPS91_TY_S_UT_66GFA285.GetValue("CJCUQTY").ToString());
            double dCHMTQTYHap = 0;

            DataRow row;

            rtnDt.Columns.Add("BLNO", typeof(System.String));   
            rtnDt.Columns.Add("CJCUQTYTOT", typeof(System.String));
            rtnDt.Columns.Add("CHCHULIL", typeof(System.String));
            rtnDt.Columns.Add("CHMTQTY", typeof(System.String));
            rtnDt.Columns.Add("CJCUQTY", typeof(System.String));
            rtnDt.Columns.Add("CJHWAJU", typeof(System.String));



            // 우선 테이타 셋의 테이블의 처음부터 끝까지 도는 루프 코딩
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                row = rtnDt.NewRow();
                if (i == 0)
                {
                    row["BLNO"] = sCHBLNO;
                    row["CJCUQTYTOT"] = Convert.ToString(dCJCUQTY);
                }
                else
                {
                    row["BLNO"] = "";
                    row["CJCUQTYTOT"] = "";
                }

                row["CHCHULIL"] = Set_Date(dt.Rows[i]["CHCHULIL"].ToString());
                row["CHMTQTY"] = dt.Rows[i]["CHMTQTY"].ToString();
                //출고량 누적 합계
                dCHMTQTYHap = dCHMTQTYHap + Convert.ToDouble(Double.Parse(Get_Numeric(dt.Rows[i]["CHMTQTY"].ToString())).ToString("#0.000"));

                row["CJCUQTY"] = Convert.ToString(Double.Parse(Convert.ToString(dCJCUQTY - dCHMTQTYHap)).ToString("#0.000"));
                row["CJHWAJU"] = sCHHWAJUNM;
                rtnDt.Rows.Add(row);
            }

            this.TXT01_CJCHQTYTOT.Text = string.Format("{0:#,##0.000}", dCHMTQTYHap);
            this.TXT01_CUJEQTYTOT.Text = string.Format("{0:#,##0.000}", dCJCUQTY - dCHMTQTYHap);

            return rtnDt;
        }
        #endregion
    }
}
