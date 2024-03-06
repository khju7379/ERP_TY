using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TY.Service.Library;
using TY.Service.Library.Controls;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.ER.GB00;

namespace TY.ER.AC00
{
    /// <summary>
    /// 부가세 마감관리 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2014.02.11 11:32
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_42BC0325 : 부가세 마감관리
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_GB_23NAD871 : 저장하시겠습니까?
    ///  TY_M_GB_23NAD873 : 저장하였습니다.
    ///  TY_M_GB_2452W459 : 저장할 데이터가 없습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  SAV : 저장
    ///  VSBRANCH : 사업장
    ///  VSCONFGB : 구분
    ///  VSRPTGUBN : 구분
    ///  VSYEAR : 기준년도
    /// </summary>
    public partial class TYACTX023I : TYBase
    {
        private string _sBUSEO = string.Empty;

        public TYACTX023I()
        {
            InitializeComponent();
        }

        private void TYACTX023I_Load(object sender, System.EventArgs e)
        {
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);

            UP_Cookie_Load();
            
            SetStartingFocus(this.TXT01_VSYEAR);

            UP_SET_BUSEO();
        }

        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();

                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_AC_42BBW322",
                    this.TXT01_VSYEAR.GetValue().ToString(),
                    this.CBO01_VSBRANCH.GetValue().ToString(),
                    getCONFGB(this.CBO01_VSRPTGUBN.GetValue().ToString(), 1),
                    getCONFGB(this.CBO01_VSRPTGUBN.GetValue().ToString(), 2)
                    );

                dt = this.DbConnector.ExecuteDataTable();

                this.FPS91_TY_S_AC_42BC0325.SetValue(dt);

                if (this.CBO01_VSBRANCH.GetValue().ToString() != "" && this.CBO01_VSRPTGUBN.GetValue().ToString() != "")
                {
                    UP_Cookie_Save();
                }
            }
            catch { 
            }
        }

        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            try
            {
                int cnt = 0;
                int i = 0;
                DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

                this.DbConnector.CommandClear();

                for (i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (UP_DATE_check(ds.Tables[0].Rows[i]["E1YEAR"].ToString(),
                                  ds.Tables[0].Rows[i]["E1RPTGUBN"].ToString() + ds.Tables[0].Rows[i]["E1CONFGB"].ToString()))
                    {
                        // 본점 마감
                        if (ds.Tables[0].Rows[i]["E1BRANCH"].ToString() == "1")
                        {
                            //권한 체크
                            if (_sBUSEO == "A20100" || _sBUSEO == "A10200")
                            {
                                // 마감
                                if (ds.Tables[0].Rows[i]["E1FINISH"].ToString() == "Y")
                                {
                                    #region Description : 지점마감 체크 제외 함 (2021.07.23 임경화 - 서성준 요청)
                                    //DataTable dt = new DataTable();

                                    //this.DbConnector.CommandClear();
                                    //this.DbConnector.Attach
                                    //    (
                                    //    "TY_P_AC_42BBW322",
                                    //    ds.Tables[0].Rows[i]["E1YEAR"].ToString(),
                                    //    "2",
                                    //    ds.Tables[0].Rows[i]["E1RPTGUBN"].ToString(),
                                    //    ds.Tables[0].Rows[i]["E1CONFGB"].ToString()
                                    //    );

                                    //dt = this.DbConnector.ExecuteDataTable();
                                    //if (dt.Rows.Count > 0)
                                    //{
                                    //    //지점 마감
                                    //    if (dt.Rows[0]["E1FINISH"].ToString() == "Y")
                                    //    {
                                    //        if ((new TYACTX024S(ds.Tables[0].Rows[i]["E1YEAR"].ToString(),
                                    //                            ds.Tables[0].Rows[i]["E1BRANCH"].ToString(),
                                    //                            ds.Tables[0].Rows[i]["E1RPTGUBN"].ToString() + ds.Tables[0].Rows[i]["E1CONFGB"].ToString()
                                    //                            )).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                                    //        {

                                    //            this.DbConnector.Attach("TY_P_AC_42B3Z337", ds.Tables[0].Rows[i]["E1FINISH"].ToString(),
                                    //                                                        ds.Tables[0].Rows[i]["E1YEAR"].ToString(),
                                    //                                                        ds.Tables[0].Rows[i]["E1BRANCH"].ToString(),
                                    //                                                        ds.Tables[0].Rows[i]["E1VENDCD"].ToString(),
                                    //                                                        ds.Tables[0].Rows[i]["E1RPTGUBN"].ToString(),
                                    //                                                        ds.Tables[0].Rows[i]["E1CONFGB"].ToString()
                                    //                                                );
                                    //            this.DbConnector.ExecuteNonQueryList();
                                    //            // 추가 2014.02.22 총괄납부자 납부환급 계산및 저장
                                    //            DataTable dt1 = new DataTable();
                                    //            this.DbConnector.CommandClear();
                                    //            this.DbConnector.Attach("TY_P_AC_44MKD232",
                                    //                                    ds.Tables[0].Rows[i]["E1YEAR"].ToString(),
                                    //                                    ds.Tables[0].Rows[i]["E1BRANCH"].ToString(),
                                    //                                    ds.Tables[0].Rows[i]["E1VENDCD"].ToString(),
                                    //                                    ds.Tables[0].Rows[i]["E1RPTGUBN"].ToString(),
                                    //                                    ds.Tables[0].Rows[i]["E1CONFGB"].ToString()
                                    //                            );
                                    //            dt1 = this.DbConnector.ExecuteDataTable();
                                    //            if (dt1.Rows.Count > 0)
                                    //            {
                                    //                this.DbConnector.Attach("TY_P_AC_44MKD231", dt1.Rows[0]["TOT25TAX"].ToString(),
                                    //                                        dt1.Rows[0]["VSYEAR"].ToString(),
                                    //                                        dt1.Rows[0]["VSBRANCH"].ToString(),
                                    //                                        dt1.Rows[0]["VSVENDCD"].ToString(),
                                    //                                        dt1.Rows[0]["VSRPTGUBN"].ToString(),
                                    //                                        dt1.Rows[0]["VSCONFGB"].ToString()
                                    //                                        );
                                    //                //this.DbConnector.ExecuteTranQueryList();
                                    //                this.DbConnector.ExecuteNonQueryList();
                                    //            }
                                    //            // 추가 --- 끝 --

                                    //            this.ShowMessage("TY_M_GB_23NAD873");
                                    //            cnt++;
                                    //        }
                                    //    }
                                    //    //지점 미마감
                                    //    else
                                    //    {
                                    //        this.ShowMessage("TY_M_AC_42D4K372");
                                    //    }
                                    //}
                                    ////지점 마감자료 X
                                    //else
                                    //{
                                    //    this.ShowMessage("TY_M_AC_42D4K372");
                                    //}
                                    #endregion


                                    if ((new TYACTX024S(ds.Tables[0].Rows[i]["E1YEAR"].ToString(),
                                                                ds.Tables[0].Rows[i]["E1BRANCH"].ToString(),
                                                                ds.Tables[0].Rows[i]["E1RPTGUBN"].ToString() + ds.Tables[0].Rows[i]["E1CONFGB"].ToString()
                                                                )).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                                    {

                                        this.DbConnector.Attach("TY_P_AC_42B3Z337", ds.Tables[0].Rows[i]["E1FINISH"].ToString(),
                                                                                    ds.Tables[0].Rows[i]["E1YEAR"].ToString(),
                                                                                    ds.Tables[0].Rows[i]["E1BRANCH"].ToString(),
                                                                                    ds.Tables[0].Rows[i]["E1VENDCD"].ToString(),
                                                                                    ds.Tables[0].Rows[i]["E1RPTGUBN"].ToString(),
                                                                                    ds.Tables[0].Rows[i]["E1CONFGB"].ToString()
                                                                            );
                                        this.DbConnector.ExecuteNonQueryList();
                                        // 추가 2014.02.22 총괄납부자 납부환급 계산및 저장
                                        DataTable dt1 = new DataTable();
                                        this.DbConnector.CommandClear();
                                        this.DbConnector.Attach("TY_P_AC_44MKD232",
                                                                ds.Tables[0].Rows[i]["E1YEAR"].ToString(),
                                                                ds.Tables[0].Rows[i]["E1BRANCH"].ToString(),
                                                                ds.Tables[0].Rows[i]["E1VENDCD"].ToString(),
                                                                ds.Tables[0].Rows[i]["E1RPTGUBN"].ToString(),
                                                                ds.Tables[0].Rows[i]["E1CONFGB"].ToString()
                                                        );
                                        dt1 = this.DbConnector.ExecuteDataTable();
                                        if (dt1.Rows.Count > 0)
                                        {
                                            this.DbConnector.Attach("TY_P_AC_44MKD231", dt1.Rows[0]["TOT25TAX"].ToString(),
                                                                    dt1.Rows[0]["VSYEAR"].ToString(),
                                                                    dt1.Rows[0]["VSBRANCH"].ToString(),
                                                                    dt1.Rows[0]["VSVENDCD"].ToString(),
                                                                    dt1.Rows[0]["VSRPTGUBN"].ToString(),
                                                                    dt1.Rows[0]["VSCONFGB"].ToString()
                                                                    );
                                            //this.DbConnector.ExecuteTranQueryList();
                                            this.DbConnector.ExecuteNonQueryList();
                                        }
                                        // 추가 --- 끝 --

                                        this.ShowMessage("TY_M_GB_23NAD873");
                                        cnt++;
                                    }
                                }
                                // 마감 취소
                                else
                                {
                                    this.DbConnector.Attach("TY_P_AC_42B3Z337", ds.Tables[0].Rows[i]["E1FINISH"].ToString(),
                                                                                ds.Tables[0].Rows[i]["E1YEAR"].ToString(),
                                                                                ds.Tables[0].Rows[i]["E1BRANCH"].ToString(),
                                                                                ds.Tables[0].Rows[i]["E1VENDCD"].ToString(),
                                                                                ds.Tables[0].Rows[i]["E1RPTGUBN"].ToString(),
                                                                                ds.Tables[0].Rows[i]["E1CONFGB"].ToString()
                                                                                );
                                    this.DbConnector.ExecuteNonQueryList();
                                    // 추가 2014.02.22 총괄납부자 납부환급 계산및 저장 (삭제시 0으로 처리)
                                    this.DbConnector.Attach("TY_P_AC_44MKD231", "0",
                                                                                ds.Tables[0].Rows[i]["E1YEAR"].ToString(),
                                                                                ds.Tables[0].Rows[i]["E1BRANCH"].ToString(),
                                                                                ds.Tables[0].Rows[i]["E1VENDCD"].ToString(),
                                                                                ds.Tables[0].Rows[i]["E1RPTGUBN"].ToString(),
                                                                                ds.Tables[0].Rows[i]["E1CONFGB"].ToString()
                                                                                );
                                    this.DbConnector.ExecuteNonQueryList();
                                    // 추가 --- 끝 --

                                    this.ShowMessage("TY_M_GB_23NAD873");
                                    cnt++;
                                }
                            }
                            else
                            {
                                this.ShowMessage("TY_M_AC_3992B618");
                            }

                        }
                        // 지점 마감
                        else if (ds.Tables[0].Rows[i]["E1BRANCH"].ToString() == "2")
                        {
                            //권한 체크
                            if (_sBUSEO == "A50300" || _sBUSEO == "A10400" || _sBUSEO == "A10300" )
                            {
                                // 마감
                                if (ds.Tables[0].Rows[i]["E1FINISH"].ToString() == "Y")
                                {
                                    if ((new TYACTX024S(ds.Tables[0].Rows[i]["E1YEAR"].ToString(),
                                                                ds.Tables[0].Rows[i]["E1BRANCH"].ToString(),
                                                                ds.Tables[0].Rows[i]["E1RPTGUBN"].ToString() + ds.Tables[0].Rows[i]["E1CONFGB"].ToString()
                                                                )).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                                    {
                                        this.DbConnector.Attach("TY_P_AC_42B3Z337", ds.Tables[0].Rows[i]["E1FINISH"].ToString(),
                                                                            ds.Tables[0].Rows[i]["E1YEAR"].ToString(),
                                                                            ds.Tables[0].Rows[i]["E1BRANCH"].ToString(),
                                                                            ds.Tables[0].Rows[i]["E1VENDCD"].ToString(),
                                                                            ds.Tables[0].Rows[i]["E1RPTGUBN"].ToString(),
                                                                            ds.Tables[0].Rows[i]["E1CONFGB"].ToString()
                                                                            );

                                        this.ShowMessage("TY_M_GB_23NAD873");
                                        cnt++;
                                    }
                                }
                                // 마감 취소
                                else
                                {
                                    DataTable dt = new DataTable();

                                    this.DbConnector.CommandClear();
                                    this.DbConnector.Attach
                                        (
                                        "TY_P_AC_42BBW322",
                                        ds.Tables[0].Rows[i]["E1YEAR"].ToString(),
                                        "1",
                                        ds.Tables[0].Rows[i]["E1RPTGUBN"].ToString(),
                                        ds.Tables[0].Rows[i]["E1CONFGB"].ToString()
                                        );

                                    dt = this.DbConnector.ExecuteDataTable();
                                    if (dt.Rows.Count > 0)
                                    {
                                        //본점 마감
                                        if (dt.Rows[0]["E1FINISH"].ToString() == "Y")
                                        {
                                            this.ShowMessage("TY_M_AC_42D4P375");
                                        }
                                        //본점 미마감
                                        else
                                        {
                                            this.DbConnector.Attach("TY_P_AC_42B3Z337", ds.Tables[0].Rows[i]["E1FINISH"].ToString(),
                                                                                        ds.Tables[0].Rows[i]["E1YEAR"].ToString(),
                                                                                        ds.Tables[0].Rows[i]["E1BRANCH"].ToString(),
                                                                                        ds.Tables[0].Rows[i]["E1VENDCD"].ToString(),
                                                                                        ds.Tables[0].Rows[i]["E1RPTGUBN"].ToString(),
                                                                                        ds.Tables[0].Rows[i]["E1CONFGB"].ToString()
                                                                                );
                                            this.ShowMessage("TY_M_GB_23NAD873");
                                            cnt++;
                                        }
                                    }
                                    //본점 마감자료 X
                                    else
                                    {
                                        this.ShowMessage("TY_M_AC_42D4P375");
                                    }
                                }
                            }
                            else
                            {
                                this.ShowMessage("TY_M_AC_3992B618");
                            }
                        }
                    }
                    else
                    {
                        this.ShowMessage("TY_M_AC_243AY315");
                    }
                }

                if (cnt > 0)
                {
                    this.DbConnector.ExecuteTranQueryList();

                    UP_Cookie_Save();
                }
                    this.BTN61_INQ_Click(null, null);
                
            }
            catch {
                
            }
        }
        #region Description : 저장 ProcessCheck 이벤트
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataSet ds = new DataSet();

            // 스프레드에서 수정 할 항목들
            ds.Tables.Add(this.FPS91_TY_S_AC_42BC0325.GetDataSourceInclude(TSpread.TActionType.Update, "E1YEAR", "E1BRANCH", "E1VENDCD", "E1RPTGUBN", "E1CONFGB", "E1FINISH"));

            if (ds.Tables[0].Rows.Count == 0)
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

        #region Description : 부서코드 가져오기
        private void UP_SET_BUSEO()
        {
            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_MR_2BEBB293",
                DateTime.Now.ToString("yyyyMMdd"),
                TYUserInfo.EmpNo
                );

            dt = this.DbConnector.ExecuteDataTable();

            this._sBUSEO = dt.Rows[0]["KBBUSEO"].ToString();
            
        }
        #endregion

        #region Description : 쿠키 불러오기
        private void UP_Cookie_Load()
        {
            if (TYCookie.Chk == "Cookie")
            {
                this.TXT01_VSYEAR.SetValue(TYCookie.Year);
                this.CBO01_VSBRANCH.SetValue(TYCookie.Branch);
                //this.CBO01_VSRPTGUBN.SetValue(TYCookie.RptGubn);
                this.CBO01_VSRPTGUBN.SetValue(TYCookie.Confgb);
            }
            else
            {
                this.TXT01_VSYEAR.SetValue(DateTime.Now.ToString("yyyyMMdd").Substring(0, 4));
            }
        }
        #endregion

        #region Description : 쿠키 저장
        private void UP_Cookie_Save()
        {
            TYCookie.Save(this.TXT01_VSYEAR.GetValue().ToString(), this.CBO01_VSBRANCH.GetValue().ToString(), this.CBO01_VSRPTGUBN.GetValue().ToString());
        }
        #endregion

        private bool UP_DATE_check(string sYEAR, string sGUBN)
        {
            //string iDATE = string.Empty;
            //int iYEAR = Convert.ToInt32(sYEAR);
            //string iMMDD = string.Empty;

            //if (sGUBN == "11")
            //{
            //    iMMDD = "0501";    
            //}
            //else if (sGUBN == "12")
            //{
            //    iMMDD = "0801";
            //}
            //else if (sGUBN == "21")
            //{
            //    iMMDD = "1101";
            //}
            //else if (sGUBN == "22")
            //{
            //    iMMDD = "0201";
            //    iYEAR++;
            //}

            //iDATE = iYEAR + iMMDD;

            //if(Convert.ToInt32(String.Format("{0:yyyy}{0:MM}{0:dd}", DateTime.Now)) >= Convert.ToInt32(iDATE)){
            //    return false;
            //}

            return true;
        }
    }
}
