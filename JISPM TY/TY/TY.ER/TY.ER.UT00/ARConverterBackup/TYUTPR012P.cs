using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using TY.Service.Library;
using TY.Service.Library.Controls;
using TY.ER.GB00;
using DataDynamics.ActiveReports;

namespace TY.ER.UT00
{
    /// <summary>
    /// 화주 화물별 입고현황 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2017.04.19 14:14
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_UT_66FD4200 : 대표 거래처 코드 조회
    ///  TY_P_UT_74JEK328 : 화주 화물별 입고현황 임시파일 삭제
    ///  TY_P_UT_74JEL329 : 화주 화물별 입고현황 임시파일 등록
    ///  TY_P_UT_74JEM330 : 화주 화물별 입고현황 임시파일 등록(화주X)
    ///  TY_P_UT_74JEM331 : 화주 화물별 입고현황 임시파일 조회(입고할증량)
    ///  TY_P_UT_74JEM332 : 화주 화물별 입고현황 임시파일 조회(입고할증량)(화주X)
    ///  TY_P_UT_74JEN333 : 화주 화물별 입고현황 임시파일 조회(통관수량)
    ///  TY_P_UT_74JEN334 : 화주 화물별 입고현황 임시파일 조회(통관수량)(화주X)
    ///  TY_P_UT_74JEN335 : 화주 화물별 입고현황 임시파일 조회(출고수량)
    ///  TY_P_UT_74JEN336 : 화주 화물별 입고현황 임시파일 조회(출고수량)(화주X)
    ///  TY_P_UT_74JEP337 : 화주 화물별 입고현황 임시파일 화주 조회
    ///  TY_P_UT_74JEP338 : 화주 화물별 입고현황 임시파일 등록(매출입고)
    ///  TY_P_UT_74JEP339 : 화주 화물별 입고현황 임시파일 수정(매출입고)
    ///  TY_P_UT_74JEP340 : 화주 화물별 입고현황 임시파일 등록(통관)
    ///  TY_P_UT_74JEQ341 : 화주 화물별 입고현황 임시파일 수정(통관)
    ///  TY_P_UT_74JEQ342 : 화주 화물별 입고현황 임시파일 등록(출고)
    ///  TY_P_UT_74JEQ343 : 화주 화물별 입고현황 임시파일 수정(출고)
    ///  TY_P_UT_74KBZ349 : 화주 화물별 입고현황 출력
    /// 
    ///  # 스프레드 정보 ####
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_2422N250 : 자료가 존재하지 않습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  PRT : 출력
    ///  CHHWAJU : 화주
    ///  EDDATE : 종료일자
    ///  STDATE : 시작일자
    /// </summary>
    public partial class TYUTPR012P : TYBase
    {
        #region Description :폼 로드
        public TYUTPR012P()
        {
            InitializeComponent();
        }

        private void TYUTPR012P_Load(object sender, System.EventArgs e)
        {
            this.DTP01_STDATE.SetValue(System.DateTime.Now.ToString("yyyy-MM-dd"));
            this.DTP01_EDDATE.SetValue(System.DateTime.Now.ToString("yyyy-MM-dd"));

            SetStartingFocus(this.DTP01_STDATE);
        }
        #endregion

        #region Descriptin : 닫기 버튼
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Descriptin : 출력 버튼
        private void BTN61_PRT_Click(object sender, EventArgs e)
        {
            string sVNCODE = string.Empty;

            sVNCODE = Get_VNCODE(this.CBH01_CHHWAJU.GetValue().ToString());
            DataTable dt = new DataTable();

            //임시파일 삭제
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_74JEK328");
            this.DbConnector.ExecuteNonQuery();

            //임시파일 등록
            if (sVNCODE != "")
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_74JEL329", DTP01_STDATE.GetString(),
                                                            DTP01_EDDATE.GetString(),
                                                            sVNCODE);
                this.DbConnector.ExecuteTranQuery();

                //입고파일 조회
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_74JEM331", DTP01_STDATE.GetString(),
                                                            DTP01_EDDATE.GetString(),
                                                            sVNCODE);
                dt = this.DbConnector.ExecuteDataTable();
            }
            else
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_74JEM330", DTP01_STDATE.GetString(),
                                                            DTP01_EDDATE.GetString());
                this.DbConnector.ExecuteTranQuery();

                //입고파일 조회
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_74JEM332", DTP01_STDATE.GetString(),
                                                            DTP01_EDDATE.GetString());
                dt = this.DbConnector.ExecuteDataTable();
            }

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                // 임시파일 화주 조회
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_74JEP337", dt.Rows[i]["COHWAJU"].ToString(),
                                                            dt.Rows[i]["COHWAMUL"].ToString());
                DataTable dt1 = this.DbConnector.ExecuteDataTable();

                if (dt1.Rows.Count > 0)
                {
                    //매출입고 UPDATE
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_UT_74JEP339", Get_Numeric(dt.Rows[i]["COOVQTY"].ToString()),
                                                                dt.Rows[i]["COHWAJU"].ToString(),
                                                                dt.Rows[i]["COHWAMUL"].ToString());
                    this.DbConnector.ExecuteTranQuery();
                }
                else
                {
                    //매출입고 INSERT
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_UT_74JEP338", dt.Rows[i]["COHWAJU"].ToString(),
                                                                dt.Rows[i]["COHWAMUL"].ToString(),
                                                                Get_Numeric(dt.Rows[i]["COOVQTY"].ToString()));
                    this.DbConnector.ExecuteTranQuery();
                }
            }

            //통관파일 조회
            if (sVNCODE != "")
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_74JEN333", DTP01_STDATE.GetString(),
                                                            DTP01_EDDATE.GetString(),
                                                            sVNCODE);
                dt = this.DbConnector.ExecuteDataTable();
            }
            else
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_74JEN334", DTP01_STDATE.GetString(),
                                                            DTP01_EDDATE.GetString());
                dt = this.DbConnector.ExecuteDataTable();
            }

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                // 임시파일 화주 조회
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_74JEP337", dt.Rows[i]["CSHWAJU"].ToString(),
                                                            dt.Rows[i]["CSHWAMUL"].ToString());
                DataTable dt1 = this.DbConnector.ExecuteDataTable();

                if (dt1.Rows.Count > 0)
                {
                    //통관 UPDATE
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_UT_74JEQ341", Get_Numeric(dt.Rows[i]["CSCUQTY"].ToString()),
                                                                dt.Rows[i]["CSHWAJU"].ToString(),
                                                                dt.Rows[i]["CSHWAMUL"].ToString());
                    this.DbConnector.ExecuteTranQuery();
                }
                else
                {
                    //통관 INSERT
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_UT_74JEP340", dt.Rows[i]["CSHWAJU"].ToString(),
                                                                dt.Rows[i]["CSHWAMUL"].ToString(),
                                                                Get_Numeric(dt.Rows[i]["CSCUQTY"].ToString()));
                    this.DbConnector.ExecuteTranQuery();
                }
            }

            //출고파일 조회
            if (sVNCODE != "")
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_74JEN335", DTP01_STDATE.GetString(),
                                                            DTP01_EDDATE.GetString(),
                                                            sVNCODE);
                dt = this.DbConnector.ExecuteDataTable();
            }
            else
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_74JEN336", DTP01_STDATE.GetString(),
                                                            DTP01_EDDATE.GetString());
                dt = this.DbConnector.ExecuteDataTable();
            }

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                // 임시파일 화주 조회
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_74JEP337", dt.Rows[i]["CHHWAJU"].ToString(),
                                                            dt.Rows[i]["CHHWAMUL"].ToString());
                DataTable dt1 = this.DbConnector.ExecuteDataTable();

                if (dt1.Rows.Count > 0)
                {
                    //출고 UPDATE
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_UT_74JEQ343", Get_Numeric(dt.Rows[i]["CHMTQTY"].ToString()),
                                                                Get_Numeric(dt.Rows[i]["CHOVQTY"].ToString()),
                                                                dt.Rows[i]["CHHWAJU"].ToString(),
                                                                dt.Rows[i]["CHHWAMUL"].ToString());
                    this.DbConnector.ExecuteTranQuery();
                }
                else
                {
                    //출고 INSERT
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_UT_74JEQ342", dt.Rows[i]["CHHWAJU"].ToString(),
                                                                dt.Rows[i]["CHHWAMUL"].ToString(),
                                                                Get_Numeric(dt.Rows[i]["CHMTQTY"].ToString()),
                                                                Get_Numeric(dt.Rows[i]["CHOVQTY"].ToString()));
                    this.DbConnector.ExecuteTranQuery();
                }
            }

            string sSTDATE = this.DTP01_STDATE.GetString(); 
            string sEDDATE = this.DTP01_EDDATE.GetString();

            string sDATE = sSTDATE.Substring(0, 4) + "/" + sSTDATE.Substring(4, 2) + "/" + sSTDATE.Substring(6, 2) + "~" + sEDDATE.Substring(0, 4) + "/" + sEDDATE.Substring(4, 2) + "/" + sEDDATE.Substring(6, 2);

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_74KBZ349", sDATE);
            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                ActiveReport rpt = new TYUTPR012R();
                // 세로 출력
                rpt.PageSettings.Orientation = DataDynamics.ActiveReports.Document.PageOrientation.Portrait;

                (new TYERGB001P(rpt, dt)).ShowDialog();
            }
            else
            {
                this.ShowMessage("TY_M_AC_2422N250");
            }
        }
        #endregion
    }
}
