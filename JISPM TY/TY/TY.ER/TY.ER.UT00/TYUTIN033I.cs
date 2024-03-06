using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library; 
using TY.Service.Library.Controls;

namespace TY.ER.UT00
{
    /// <summary>
    /// 선급자재 관리 프로그램입니다.
    /// 
    /// 작성자 : 김상권
    /// 작성일 : 2013.02.19 09:59
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_MR_32J79125 : 선급자재 미생성 조회
    ///  TY_P_MR_32J7A126 : 선급자재 생성 조회
    ///  TY_P_MR_32J7A127 : 선급자재 DETAIL 조회
    ///  TY_P_MR_32J7A128 : 선급자재 DETAIL 하위 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_MR_32J7C129 : 선급자재 생성 조회
    ///  TY_S_MR_32J7M130 : 선급자재 DETAIL 조회
    ///  TY_S_UT_71OBG584 : 선급자재 DETAIL 하위 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  CANCEL : 취소
    ///  CREATE : 생성
    ///  INQ : 조회
    ///  JASAN_CRE : 자산생성
    ///  JASAN_DEL : 자산삭제
    ///  JPNO_CRE : 전표생성
    ///  JPNO_DEL : 전표삭제
    ///  FXDDPMK : 귀속부서
    ///  FXDSAUP : 선급사업부
    ///  FXDGETDATE : 취득일
    ///  GCDACGHAP : 계정총액
    ///  GDAESANGHAP : 대상총액
    ///  GJANGHAP : 잔액
    /// </summary>
    public partial class TYUTIN033I : TYBase
    {
        #region Description : 페이지 로드
        public TYUTIN033I()
        {
            InitializeComponent();

            #region Description : 입고 및 강제 입고
            
            // 화주
            this.SetSpreadCodeHelper(this.FPS91_TY_S_UT_71OBG584, "CJHWAJU", "HJDESC1", "CJHWAJU");
            // 화주
            this.SetSpreadCodeHelper(this.FPS91_TY_S_UT_71OBG584, "CJHWAMUL", "HMDESC1", "CJHWAMUL");
            // 소속
            this.SetSpreadCodeHelper(this.FPS91_TY_S_UT_71OBG584, "CJSOSOK",  "SKDESC1", "CJSOSOK");
            // 통관화주
            this.SetSpreadCodeHelper(this.FPS91_TY_S_UT_71OBG584, "CJACTHJ", "VNSANGHO", "CJACTHJ");
            // 도착지
            this.SetSpreadCodeHelper(this.FPS91_TY_S_UT_71OBG584, "CJCHHJ", "DCDESC1", "CJCHHJ");

            #endregion

            #region Description : 강제출고

            // 화주
            this.SetSpreadCodeHelper(this.FPS91_TY_S_UT_71OCR586, "CJHWAJU", "HJDESC1", "CJHWAJU");
            // 화주
            this.SetSpreadCodeHelper(this.FPS91_TY_S_UT_71OCR586, "CJHWAMUL", "HMDESC1", "CJHWAMUL");
            // 소속
            this.SetSpreadCodeHelper(this.FPS91_TY_S_UT_71OCR586, "CJSOSOK", "SKDESC1", "CJSOSOK");
            // 통관화주
            this.SetSpreadCodeHelper(this.FPS91_TY_S_UT_71OCR586, "CJACTHJ", "VNSANGHO", "CJACTHJ");
            // 도착지
            this.SetSpreadCodeHelper(this.FPS91_TY_S_UT_71OCR586, "CJCHHJ", "DCDESC1", "CJCHHJ");

            #endregion
        }

        private void TYUTIN033I_Load(object sender, System.EventArgs e)
        {
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);

            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            this.FPS91_TY_S_UT_71OBG584.Initialize();
            this.FPS91_TY_S_UT_71OCR586.Initialize();

            SetStartingFocus(this.DTP01_STDATE);

            this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            this.FPS91_TY_S_UT_71OBG584.Initialize();
            this.FPS91_TY_S_UT_71OCR586.Initialize();

            if (this.CBO01_GGUBUN.GetValue().ToString() == "A" || this.CBO01_GGUBUN.GetValue().ToString() == "I")
            {
                if (this.CBO01_CHGUBUN.GetValue().ToString() == "1")
                {
                    // 강제계근 입고조회(입고 등록)
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_UT_71OBG583",
                        Get_Date(this.DTP01_STDATE.GetValue().ToString()),
                        Get_Date(this.DTP01_EDDATE.GetValue().ToString()),
                        this.CBH01_SHWAJU.GetValue().ToString(),
                        this.CBH01_SHWAMUL.GetValue().ToString()
                        );
                }
                else if (this.CBO01_CHGUBUN.GetValue().ToString() == "2")
                {
                    // 강제계근 입고조회(입고 미등록)
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_UT_71PAV592",
                        Get_Date(this.DTP01_STDATE.GetValue().ToString()),
                        Get_Date(this.DTP01_EDDATE.GetValue().ToString()),
                        this.CBH01_SHWAJU.GetValue().ToString(),
                        this.CBH01_SHWAMUL.GetValue().ToString()
                        );
                }
                else if (this.CBO01_CHGUBUN.GetValue().ToString() == "3")
                {
                    // 강제계근 입고조회(입고 미등록)
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_UT_71PAW593",
                        Get_Date(this.DTP01_STDATE.GetValue().ToString()),
                        Get_Date(this.DTP01_EDDATE.GetValue().ToString()),
                        this.CBH01_SHWAJU.GetValue().ToString(),
                        this.CBH01_SHWAMUL.GetValue().ToString()
                        );
                }

                dt = this.DbConnector.ExecuteDataTable();

                this.FPS91_TY_S_UT_71OBG584.SetValue(dt);
            }

            if (this.CBO01_GGUBUN.GetValue().ToString() == "A" || this.CBO01_GGUBUN.GetValue().ToString() == "C")
            {
                // 강제계근 출고조회
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_UT_71OC1585",
                    Get_Date(this.DTP01_STDATE.GetValue().ToString()),
                    Get_Date(this.DTP01_EDDATE.GetValue().ToString()),
                    this.CBH01_SHWAJU.GetValue().ToString(),
                    this.CBH01_SHWAMUL.GetValue().ToString()
                    );

                dt = this.DbConnector.ExecuteDataTable();

                this.FPS91_TY_S_UT_71OCR586.SetValue(dt);
            }
        }
        #endregion

        #region Description : 저장 버튼
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            string sCJIPQTY  = string.Empty;
            string sCJIPTIME = string.Empty;
            string sCJCHTIME = string.Empty;
            string sCJNUMBER = string.Empty;

            string sCJMTQTY  = string.Empty;

            int i = 0;

            DataTable dt = new DataTable();

            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            #region Description : 입고 및 강제 입고

            for (i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                sCJNUMBER = "";

                sCJIPTIME = Set_Fill2(ds.Tables[0].Rows[i]["CJIPTIME1"].ToString()) + Set_Fill2(ds.Tables[0].Rows[i]["CJIPTIME2"].ToString());
                sCJCHTIME = Set_Fill2(ds.Tables[0].Rows[i]["CJCHTIME1"].ToString()) + Set_Fill2(ds.Tables[0].Rows[i]["CJCHTIME2"].ToString());

                sCJIPQTY = (
                            (
                              double.Parse(String.Format("{0,9:N3}", Get_Numeric(ds.Tables[0].Rows[i]["CJEMPTY"].ToString())))
                            - double.Parse(String.Format("{0,9:N3}", Get_Numeric(ds.Tables[0].Rows[i]["CJTOTAL"].ToString())))
                            ).ToString("0.000"));

                // 차량번호
                this.DbConnector.CommandClear();
                this.DbConnector.Attach(
                                       "TY_P_UT_6BAH0732",
                                       ds.Tables[0].Rows[i]["CJCARNO"].ToString()
                                       );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    sCJNUMBER = dt.Rows[0]["TRNUMNO2"].ToString();
                }
                else
                {
                    sCJNUMBER = ds.Tables[0].Rows[i]["CJCARNO"].ToString();
                }

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_CCRBY437", ds.Tables[0].Rows[i]["CJHWAJU"].ToString(),
                                                            ds.Tables[0].Rows[i]["CJHWAMUL"].ToString(),
                                                            Set_TankNo(ds.Tables[0].Rows[i]["CJTANKNO1"].ToString()),
                                                            sCJNUMBER.ToString(),
                                                            ds.Tables[0].Rows[i]["CJSOSOK"].ToString(),
                                                            ds.Tables[0].Rows[i]["CJEMPTY"].ToString(),
                                                            ds.Tables[0].Rows[i]["CJTOTAL"].ToString(),
                                                            sCJIPQTY.ToString(),
                                                            sCJIPQTY.ToString(),
                                                            sCJIPTIME.ToString(),
                                                            sCJCHTIME.ToString(),
                                                            ds.Tables[0].Rows[i]["CJCONTAIN"].ToString(),
                                                            ds.Tables[0].Rows[i]["CJSEALNUM"].ToString(),
                                                            ds.Tables[0].Rows[i]["CJCARNO"].ToString(),
                                                            ds.Tables[0].Rows[i]["CJACTHJ"].ToString(),
                                                            ds.Tables[0].Rows[i]["CJCHHJ"].ToString(),
                                                            TYUserInfo.EmpNo,
                                                            ds.Tables[0].Rows[i]["CJCHULIL"].ToString(),
                                                            ds.Tables[0].Rows[i]["CJTKNO"].ToString(),
                                                            ds.Tables[0].Rows[i]["CJJISINO1"].ToString()                                                            
                                                            ); //저장
                this.DbConnector.ExecuteNonQuery();
            }

            #endregion

            #region Description : 출고

            for (i = 0; i < ds.Tables[1].Rows.Count; i++)
            {
                sCJNUMBER = "";

                sCJIPTIME = Set_Fill2(ds.Tables[1].Rows[i]["CJIPTIME1"].ToString().Substring(0, 2)) + Set_Fill2(ds.Tables[1].Rows[i]["CJIPTIME1"].ToString().Substring(3, 2));
                sCJCHTIME = Set_Fill2(ds.Tables[1].Rows[i]["CJCHTIME1"].ToString().Substring(0, 2)) + Set_Fill2(ds.Tables[1].Rows[i]["CJCHTIME1"].ToString().Substring(3, 2));

                sCJMTQTY = (
                            (
                              double.Parse(String.Format("{0,9:N3}", Get_Numeric(ds.Tables[1].Rows[i]["CJTOTAL"].ToString())))
                            - double.Parse(String.Format("{0,9:N3}", Get_Numeric(ds.Tables[1].Rows[i]["CJEMPTY"].ToString())))
                            ).ToString("0.000"));

                // 차량번호
                this.DbConnector.CommandClear();
                this.DbConnector.Attach(
                                       "TY_P_UT_6BAH0732",
                                       ds.Tables[1].Rows[i]["CJCARNO"].ToString()
                                       );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    sCJNUMBER = dt.Rows[0]["TRNUMNO2"].ToString();
                }
                else
                {
                    sCJNUMBER = ds.Tables[1].Rows[i]["CJCARNO"].ToString();
                }


                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_81NGY519", ds.Tables[1].Rows[i]["CJHWAJU"].ToString(),
                                                            ds.Tables[1].Rows[i]["CJHWAMUL"].ToString(),
                                                            Set_TankNo(ds.Tables[1].Rows[i]["CJTANKNO1"].ToString()),
                                                            sCJNUMBER.ToString(),
                                                            ds.Tables[1].Rows[i]["CJSOSOK"].ToString(),
                                                            ds.Tables[1].Rows[i]["CJEMPTY"].ToString(),
                                                            ds.Tables[1].Rows[i]["CJTOTAL"].ToString(),
                                                            sCJMTQTY.ToString(),
                                                            ds.Tables[1].Rows[i]["CJCONTAIN"].ToString(),
                                                            ds.Tables[1].Rows[i]["CJSEALNUM"].ToString(),
                                                            ds.Tables[1].Rows[i]["CJCARNO"].ToString(),
                                                            ds.Tables[1].Rows[i]["CJACTHJ"].ToString(),
                                                            ds.Tables[1].Rows[i]["CJCHHJ"].ToString(),
                                                            TYUserInfo.EmpNo,
                                                            ds.Tables[1].Rows[i]["CJCHULIL"].ToString(),
                                                            ds.Tables[1].Rows[i]["CJTKNO"].ToString()
                                                            ); //저장
                this.DbConnector.ExecuteNonQuery();
            }


            #endregion

            this.BTN61_INQ_Click(null, null);
            this.ShowMessage("TY_M_GB_23NAD873"); // 저장 메세지
        }
        #endregion

        #region Description : 삭제 버튼
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            int i = 0;

            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            #region Description : 입고 및 강제 입고

            if (ds.Tables[0].Rows.Count > 0)
            {
                this.DbConnector.CommandClear();

                for (i = 0; i < ds.Tables[0].Rows.Count; i++)
                {

                    this.DbConnector.Attach("TY_P_UT_82NE5629", ds.Tables[0].Rows[i]["CJCHULIL"].ToString(),
                                                                ds.Tables[0].Rows[i]["CJTKNO"].ToString(),
                                                                ds.Tables[0].Rows[i]["CJJISINO1"].ToString()
                                                                ); // 삭제
                }

                this.DbConnector.ExecuteTranQueryList();
            }
            

            #endregion

            #region Description : 출고

            if (ds.Tables[1].Rows.Count > 0)
            {
                this.DbConnector.CommandClear();
                
                for (i = 0; i < ds.Tables[1].Rows.Count; i++)
                {
                    this.DbConnector.Attach("TY_P_UT_82NE7630", ds.Tables[1].Rows[i]["CJCHULIL"].ToString(),
                                                                ds.Tables[1].Rows[i]["CJTKNO"].ToString()
                                                                ); // 삭제
                }

                this.DbConnector.ExecuteTranQueryList();
            }

            #endregion

            this.BTN61_INQ_Click(null, null);
            this.ShowMessage("TY_M_GB_23NAD874"); // 저장 메세지
        }
        #endregion

        #region Description : 저장 ProcessCheck
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataSet ds = new DataSet();

            DataTable dt = new DataTable();

            // 입고 및 강제 입고 스프레드에서 수정 할 항목들
            ds.Tables.Add(this.FPS91_TY_S_UT_71OBG584.GetDataSourceInclude(TSpread.TActionType.Update, "CJCHULIL",  "CJTKNO",    "CJHWAJU",   "CJHWAMUL",  "CJTANKNO1", "CJCARNO", "CJCONTAIN", "CJSEALNUM", "CJSOSOK",
                                                                                                       "CJIPTIME1", "CJIPTIME2", "CJCHTIME1", "CJCHTIME2", "CJEMPTY",   "CJTOTAL", "CJIPQTY",   "CJJISINO1", "CJACTHJ", "CJCHHJ"));

            // 강제 출고 스프레드에서 수정 할 항목들
            ds.Tables.Add(this.FPS91_TY_S_UT_71OCR586.GetDataSourceInclude(TSpread.TActionType.Update, "CJCHULIL",  "CJTKNO",    "CJHWAJU", "CJHWAMUL", "CJTANKNO1", "CJCARNO", "CJCONTAIN", "CJSEALNUM", "CJSOSOK",
                                                                                                       "CJIPTIME1", "CJCHTIME1", "CJEMPTY", "CJTOTAL",  "CJMTQTY",   "CJACTHJ", "CJCHHJ"));

            if (ds.Tables[0].Rows.Count == 0 && ds.Tables[1].Rows.Count == 0)
            {
                this.ShowMessage("TY_M_GB_2452W459");
                e.Successed = false;
                return;
            }

            // 수정
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (ds.Tables[0].Rows[i]["CJHWAJU"].ToString() == "")
                {
                    this.ShowMessage("TY_M_UT_71H9N475");
                    e.Successed = false;
                    return;
                }

                if (ds.Tables[0].Rows[i]["CJHWAMUL"].ToString() == "")
                {
                    this.ShowMessage("TY_M_AC_387AH358");
                    e.Successed = false;
                    return;
                }


                if (ds.Tables[0].Rows[i]["CJTANKNO1"].ToString() == "")
                {
                    this.ShowMessage("TY_M_UT_66SDG425");
                    e.Successed = false;
                    return;
                }

                // 실차 수량
                if (double.Parse(Get_Numeric(ds.Tables[0].Rows[i]["CJEMPTY"].ToString())) == 0)
                {
                    this.ShowMessage("TY_M_UT_81NH6521");
                    e.Successed = false;
                    return;
                }

                // 공차 수량
                if (double.Parse(Get_Numeric(ds.Tables[0].Rows[i]["CJTOTAL"].ToString())) == 0)
                {
                    this.ShowMessage("TY_M_UT_81NH6520");
                    e.Successed = false;
                    return;
                }

                //// 차량번호
                //this.DbConnector.CommandClear();
                //this.DbConnector.Attach(
                //                       "TY_P_UT_6BAH0732",
                //                       ds.Tables[0].Rows[i]["CJCARNO"].ToString()
                //                       );

                //dt = this.DbConnector.ExecuteDataTable();

                //if (dt.Rows.Count <= 0)
                //{
                //    this.ShowMessage("TY_M_UT_71NBW561");
                //    e.Successed = false;
                //    return;
                //}
            }


            // 수정
            for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
            {
                if (ds.Tables[1].Rows[i]["CJHWAJU"].ToString() == "")
                {
                    this.ShowMessage("TY_M_UT_71H9N475");
                    e.Successed = false;
                    return;
                }

                if (ds.Tables[1].Rows[i]["CJHWAMUL"].ToString() == "")
                {
                    this.ShowMessage("TY_M_AC_387AH358");
                    e.Successed = false;
                    return;
                }


                if (ds.Tables[1].Rows[i]["CJTANKNO1"].ToString() == "")
                {
                    this.ShowMessage("TY_M_UT_66SDG425");
                    e.Successed = false;
                    return;
                }

                // 공차 수량
                if (double.Parse(Get_Numeric(ds.Tables[1].Rows[i]["CJEMPTY"].ToString())) == 0)
                {
                    this.ShowMessage("TY_M_UT_81NH6520");
                    e.Successed = false;
                    return;
                }

                // 실차 수량
                if (double.Parse(Get_Numeric(ds.Tables[1].Rows[i]["CJTOTAL"].ToString())) == 0)
                {
                    this.ShowMessage("TY_M_UT_81NH6521");
                    e.Successed = false;
                    return;
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

        #region Description : 삭제 ProcessCheck
        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataSet ds = new DataSet();

            DataTable dt = new DataTable();

            // 입고 및 강제 입고 스프레드에서 삭제 할 항목들
            ds.Tables.Add(this.FPS91_TY_S_UT_71OBG584.GetDataSourceInclude(TSpread.TActionType.Remove, "CJCHULIL", "CJTKNO", "CJJISINO1"));

            // 강제 출고 스프레드에서 수정 할 항목들
            ds.Tables.Add(this.FPS91_TY_S_UT_71OCR586.GetDataSourceInclude(TSpread.TActionType.Remove, "CJCHULIL", "CJTKNO"));

            if (ds.Tables[0].Rows.Count == 0 && ds.Tables[1].Rows.Count == 0)
            {
                this.ShowMessage("TY_M_GB_23NAD870");
                e.Successed = false;
                return;
            }

            // 입고 삭제
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (Get_Numeric(ds.Tables[0].Rows[i]["CJJISINO1"].ToString()) != "0")
                {
                    this.ShowMessage("TY_M_UT_82NE3628");
                    e.Successed = false;
                    return;
                }
            }            

            e.ArgData = ds;
        }
        #endregion

        #region Description : 차량번호 코드헬프
        private void FPS91_TY_S_UT_71OBG584_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == System.Windows.Forms.Keys.F1) && this.FPS91_TY_S_UT_71OBG584.ActiveSheet.ActiveColumnIndex == 7)
            {
                TYUTGB011S popup = new TYUTGB011S(this.FPS91_TY_S_UT_71OBG584.GetValue("CJCARNO").ToString());

                if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    this.FPS91_TY_S_UT_71OBG584.SetValue(this.FPS91_TY_S_UT_71OBG584.ActiveSheet.ActiveRowIndex, "CJCARNO", popup.fsCARNUMBER);

                    this.FPS91_TY_S_UT_71OBG584.ActiveSheet.RowHeader.Cells[this.FPS91_TY_S_UT_71OBG584.ActiveSheet.ActiveRowIndex, 0].Text = "U";
                }
            }
        }
        #endregion
    }
}