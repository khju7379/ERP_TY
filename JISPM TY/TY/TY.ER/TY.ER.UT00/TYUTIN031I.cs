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
    ///  TY_S_UT_71JG1546 : 선급자재 DETAIL 하위 조회
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
    public partial class TYUTIN031I : TYBase
    {
        private string fsFXDNUM = string.Empty;

        private string fsJASANNUM = string.Empty;
        private string fsPONUM    = string.Empty;
        private string fsRRNUM    = string.Empty;
        private string fsVEND     = string.Empty;
        private string fsITEMCODE = string.Empty;
        private string fsCGVEND   = string.Empty;
        private string fsCHGUBUN  = string.Empty;
        private string fsGUBUN    = string.Empty;

        private string fsIJWKTYPE  = string.Empty;
        private string fsIJTMGUBN  = string.Empty;
        private string fsIJIPDATE  = string.Empty;

        private string fsIJHWAJU   = string.Empty;
        private string fsIJHWAMUL  = string.Empty;
        private string fsIJTANKNO  = string.Empty;
        private string fsIJIPQTY   = string.Empty;

        private string fsIJCARNO   = string.Empty;
        private string fsIJCONTAIN = string.Empty;
        private string fsIJSEALNUM = string.Empty;

        private string fsIJIPTIME1 = string.Empty;
        private string fsIJIPTIME2 = string.Empty;

        private string fsIJDESC    = string.Empty;

        private string fsHJDESC1   = string.Empty;
        private string fsHMDESC1   = string.Empty;

        #region Description : 페이지 로드
        public TYUTIN031I()
        {
            InitializeComponent();

            // 화주
            this.SetSpreadCodeHelper(this.FPS91_TY_S_UT_71JG1546, "IJHWAJU", "HJDESC1", "IJHWAJU");
            // 화물
            this.SetSpreadCodeHelper(this.FPS91_TY_S_UT_71JG1546, "IJHWAMUL", "HMDESC1", "IJHWAMUL");
            // 차량번호
            //this.SetSpreadCodeHelper(this.FPS91_TY_S_UT_71JG1546, "IJCARNO", "IJCARNO", "IJCARNO");
        }

        private void TYUTIN031I_Load(object sender, System.EventArgs e)
        {
            // Key필드 수정모드시 잠금
            this.SetSpreadKeyColumn(this.FPS91_TY_S_UT_71JG1546, "IJIPDATE");

            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            this.FPS91_TY_S_UT_71JG1546.Initialize();

            this.BTN61_INQ_Click(null, null);

            SetStartingFocus(this.DTP01_STDATE);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            this.DbConnector.Attach
                (
                "TY_P_UT_71JEV541",
                Get_Date(this.DTP01_STDATE.GetValue().ToString()),
                Get_Date(this.DTP01_EDDATE.GetValue().ToString()),
                this.CBH01_SHWAJU.GetValue().ToString(),
                this.CBH01_SHWAMUL.GetValue().ToString(),
                this.CBO01_IJWKTYPE.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_UT_71JG1546.SetValue(dt);
        }
        #endregion

        #region Description : 저장 버튼
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            string sIJIPTIME = string.Empty;
            string sIJNUMBER = string.Empty;

            int i = 0;

            DataTable dt = new DataTable();

            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            for (i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                sIJIPTIME = Set_Fill2(ds.Tables[0].Rows[i]["IJIPTIME1"].ToString()) + Set_Fill2(ds.Tables[0].Rows[i]["IJIPTIME2"].ToString());

                if (ds.Tables[0].Rows[i]["IJCARNO"].ToString() != "")
                {
                    // 차량번호
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach(
                                           "TY_P_UT_6BAH0732",
                                           ds.Tables[0].Rows[i]["IJCARNO"].ToString()
                                           );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        sIJNUMBER = dt.Rows[0]["TRNUMNO2"].ToString();
                    }
                }

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_71JET538", ds.Tables[0].Rows[i]["IJIPDATE"].ToString(),
                                                            ds.Tables[0].Rows[i]["IJIPDATE"].ToString(),
                                                            ds.Tables[0].Rows[i]["IJHWAJU"].ToString(),
                                                            ds.Tables[0].Rows[i]["IJHWAMUL"].ToString(),
                                                            Set_TankNo(ds.Tables[0].Rows[i]["IJTANKNO"].ToString()),
                                                            ds.Tables[0].Rows[i]["IJIPQTY"].ToString(),
                                                            ds.Tables[0].Rows[i]["IJCARNO"].ToString(),
                                                            ds.Tables[0].Rows[i]["IJCONTAIN"].ToString(),
                                                            ds.Tables[0].Rows[i]["IJSEALNUM"].ToString(),
                                                            ds.Tables[0].Rows[i]["IJWKTYPE"].ToString(),
                                                            ds.Tables[0].Rows[i]["IJTMGUBN"].ToString(),
                                                            sIJIPTIME.ToString(),
                                                            ds.Tables[0].Rows[i]["IJDESC"].ToString(),
                                                            sIJNUMBER.ToString(),
                                                            TYUserInfo.EmpNo
                                                            ); //저장
                this.DbConnector.ExecuteNonQuery();
            }

            if (ds.Tables[1].Rows.Count > 0)
            {                
                for (i = 0; i < ds.Tables[1].Rows.Count; i++)
                {
                    sIJIPTIME = Set_Fill2(ds.Tables[1].Rows[i]["IJIPTIME1"].ToString()) + Set_Fill2(ds.Tables[1].Rows[i]["IJIPTIME2"].ToString());


                    if (ds.Tables[1].Rows[i]["IJCARNO"].ToString() != "")
                    {
                        // 차량번호
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach(
                                               "TY_P_UT_6BAH0732",
                                               ds.Tables[1].Rows[i]["IJCARNO"].ToString()
                                               );

                        dt = this.DbConnector.ExecuteDataTable();

                        if (dt.Rows.Count > 0)
                        {
                            sIJNUMBER = dt.Rows[0]["TRNUMNO2"].ToString();
                        }
                    }

                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_UT_71JET539", ds.Tables[1].Rows[i]["IJHWAJU"].ToString(),
                                                                ds.Tables[1].Rows[i]["IJHWAMUL"].ToString(),
                                                                Set_TankNo(ds.Tables[1].Rows[i]["IJTANKNO"].ToString()),
                                                                ds.Tables[1].Rows[i]["IJIPQTY"].ToString(),
                                                                ds.Tables[1].Rows[i]["IJCARNO"].ToString(),
                                                                ds.Tables[1].Rows[i]["IJCONTAIN"].ToString(),
                                                                ds.Tables[1].Rows[i]["IJSEALNUM"].ToString(),
                                                                ds.Tables[1].Rows[i]["IJWKTYPE"].ToString(),
                                                                ds.Tables[1].Rows[i]["IJTMGUBN"].ToString(),
                                                                sIJIPTIME.ToString(),
                                                                ds.Tables[1].Rows[i]["IJDESC"].ToString(),
                                                                sIJNUMBER.ToString(),
                                                                TYUserInfo.EmpNo,
                                                                ds.Tables[1].Rows[i]["IJIPDATE"].ToString(),
                                                                ds.Tables[1].Rows[i]["IJTKNO"].ToString()
                                                                ); //저장
                    this.DbConnector.ExecuteNonQuery();
                }
            }

            this.BTN61_INQ_Click(null, null);
            this.ShowMessage("TY_M_GB_23NAD873"); // 저장 메세지
        }
        #endregion

        #region Description : 삭제 버튼
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

            this.DbConnector.CommandClear();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                this.DbConnector.Attach("TY_P_UT_71JET540", dt.Rows[i]["IJIPDATE"].ToString(),
                                                            dt.Rows[i]["IJTKNO"].ToString()
                                                            );

                this.DbConnector.Attach("TY_P_UT_81HGJ481", dt.Rows[i]["IJIPDATE"].ToString(),
                                                            dt.Rows[i]["IJTKNO"].ToString()
                                                            );
            }

            this.DbConnector.ExecuteTranQueryList();

            this.BTN61_INQ_Click(null, null);
            this.ShowMessage("TY_M_GB_23NAD874"); // 삭제 메세지
        }
        #endregion

        #region Description : 저장 ProcessCheck
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataSet ds = new DataSet();

            DataTable dt = new DataTable();

            // 스프레드에서 등록 할 항목들
            ds.Tables.Add(this.FPS91_TY_S_UT_71JG1546.GetDataSourceInclude(TSpread.TActionType.New,    "IJIPDATE", "IJWKTYPE", "IJHWAJU", "IJHWAMUL", "IJTANKNO", "IJIPQTY", "IJCARNO", "IJCONTAIN", "IJSEALNUM", "IJTMGUBN", "IJIPTIME1", "IJIPTIME2", "IJDESC"));
            // 스프레드에서 수정 할 항목들
            ds.Tables.Add(this.FPS91_TY_S_UT_71JG1546.GetDataSourceInclude(TSpread.TActionType.Update, "IJIPDATE", "IJTKNO", "IJWKTYPE", "IJHWAJU", "IJHWAMUL", "IJTANKNO", "IJIPQTY", "IJCARNO", "IJCONTAIN", "IJSEALNUM", "IJTMGUBN", "IJIPTIME1", "IJIPTIME2", "IJDESC"));

            if (ds.Tables[0].Rows.Count == 0 && ds.Tables[1].Rows.Count == 0)
            {
                this.ShowMessage("TY_M_GB_2452W459");
                e.Successed = false;
                return;
            }

            // 신규
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                // 화주
                this.DbConnector.CommandClear();
                this.DbConnector.Attach(
                                       "TY_P_UT_668DS093",
                                       "HJ",
                                       ds.Tables[0].Rows[i]["IJHWAJU"].ToString()
                                       );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count <= 0)
                {
                    this.ShowMessage("TY_M_UT_71NBQ558");
                    e.Successed = false;
                    return;
                }

                // 화물
                this.DbConnector.CommandClear();
                this.DbConnector.Attach(
                                       "TY_P_UT_668DS093",
                                       "HM",
                                       ds.Tables[0].Rows[i]["IJHWAMUL"].ToString()
                                       );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count <= 0)
                {
                    this.ShowMessage("TY_M_UT_71NBR559");
                    e.Successed = false;
                    return;
                }

                // 탱크번호
                this.DbConnector.CommandClear();
                this.DbConnector.Attach(
                                       "TY_P_UT_66SDH426",
                                       ds.Tables[0].Rows[i]["IJTANKNO"].ToString().Trim()
                                       );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count <= 0)
                {
                    this.ShowMessage("TY_M_UT_6AKBV437");
                    e.Successed = false;
                    return;
                }


                if (ds.Tables[0].Rows[i]["IJWKTYPE"].ToString() == "01")
                {
                    if (ds.Tables[0].Rows[i]["IJCARNO"].ToString() == "")
                    {
                        this.ShowMessage("TY_M_UT_7AHGM834");
                        e.Successed = false;
                        return;
                    }
                    else
                    {
                        // 차량번호
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach(
                                               "TY_P_UT_6BAH0732",
                                               ds.Tables[0].Rows[i]["IJCARNO"].ToString()
                                               );

                        dt = this.DbConnector.ExecuteDataTable();

                        if (dt.Rows.Count <= 0)
                        {
                            this.ShowMessage("TY_M_UT_71NBW561");
                            e.Successed = false;
                            return;
                        }
                    }
                }
                else
                {
                    //if (ds.Tables[0].Rows[i]["IJSEALNUM"].ToString() == "")
                    //{
                    //    this.ShowMessage("TY_M_UT_7AHHR836");
                    //    e.Successed = false;
                    //    return;
                    //}

                    if (ds.Tables[0].Rows[i]["IJCONTAIN"].ToString() == "")
                    {
                        this.ShowMessage("TY_M_UT_7AHGM833");
                        e.Successed = false;
                        return;
                    }
                    else
                    {
                        if (ds.Tables[0].Rows[i]["IJCONTAIN"].ToString().Length != 11)
                        {
                            this.ShowMessage("TY_M_UT_7AHHI835");
                            e.Successed = false;
                            return;
                        }

                        string sCONTNUM = string.Empty;

                        sCONTNUM = ds.Tables[0].Rows[i]["IJCONTAIN"].ToString();

                        for (int j = 0; j < 11; j++)
                        {
                            if (j == 0 || j == 1 || j == 2 || j == 3)
                            {
                                if (sCONTNUM.Substring(j, 1).ToString() != "A" &&
                                    sCONTNUM.Substring(j, 1).ToString() != "B" &&
                                    sCONTNUM.Substring(j, 1).ToString() != "C" &&
                                    sCONTNUM.Substring(j, 1).ToString() != "D" &&
                                    sCONTNUM.Substring(j, 1).ToString() != "E" &&
                                    sCONTNUM.Substring(j, 1).ToString() != "F" &&
                                    sCONTNUM.Substring(j, 1).ToString() != "G" &&
                                    sCONTNUM.Substring(j, 1).ToString() != "H" &&
                                    sCONTNUM.Substring(j, 1).ToString() != "I" &&
                                    sCONTNUM.Substring(j, 1).ToString() != "J" &&
                                    sCONTNUM.Substring(j, 1).ToString() != "K" &&
                                    sCONTNUM.Substring(j, 1).ToString() != "L" &&
                                    sCONTNUM.Substring(j, 1).ToString() != "M" &&
                                    sCONTNUM.Substring(j, 1).ToString() != "N" &&
                                    sCONTNUM.Substring(j, 1).ToString() != "O" &&
                                    sCONTNUM.Substring(j, 1).ToString() != "P" &&
                                    sCONTNUM.Substring(j, 1).ToString() != "Q" &&
                                    sCONTNUM.Substring(j, 1).ToString() != "R" &&
                                    sCONTNUM.Substring(j, 1).ToString() != "S" &&
                                    sCONTNUM.Substring(j, 1).ToString() != "T" &&
                                    sCONTNUM.Substring(j, 1).ToString() != "U" &&
                                    sCONTNUM.Substring(j, 1).ToString() != "V" &&
                                    sCONTNUM.Substring(j, 1).ToString() != "W" &&
                                    sCONTNUM.Substring(j, 1).ToString() != "X" &&
                                    sCONTNUM.Substring(j, 1).ToString() != "Y" &&
                                    sCONTNUM.Substring(j, 1).ToString() != "Z")
                                {
                                    this.ShowMessage("TY_M_UT_7AHGJ830");
                                    e.Successed = false;
                                    return;
                                }
                            }
                            else
                            {

                                if (sCONTNUM.Substring(j, 1).ToString() != "0" &&
                                    sCONTNUM.Substring(j, 1).ToString() != "1" &&
                                    sCONTNUM.Substring(j, 1).ToString() != "2" &&
                                    sCONTNUM.Substring(j, 1).ToString() != "3" &&
                                    sCONTNUM.Substring(j, 1).ToString() != "4" &&
                                    sCONTNUM.Substring(j, 1).ToString() != "5" &&
                                    sCONTNUM.Substring(j, 1).ToString() != "6" &&
                                    sCONTNUM.Substring(j, 1).ToString() != "7" &&
                                    sCONTNUM.Substring(j, 1).ToString() != "8" &&
                                    sCONTNUM.Substring(j, 1).ToString() != "9")
                                {
                                    this.ShowMessage("TY_M_UT_7AHGJ832");
                                    e.Successed = false;
                                    return;
                                }
                            }
                        }
                    }
                }
            }

            string sCJJISINO1 = string.Empty;

            // 수정
            for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
            {
                sCJJISINO1 = Get_Date(ds.Tables[1].Rows[i]["IJIPDATE"].ToString()) + Set_Fill3(ds.Tables[1].Rows[i]["IJTKNO"].ToString());

                // 강제 계근 자료 체크
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_UT_71NDE562",
                    sCJJISINO1
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.ShowMessage("TY_M_UT_71NDI563");

                    e.Successed = false;
                    return;
                }

                // 화주
                this.DbConnector.CommandClear();
                this.DbConnector.Attach(
                                       "TY_P_UT_668DS093",
                                       "HJ",
                                       ds.Tables[1].Rows[i]["IJHWAJU"].ToString()
                                       );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count <= 0)
                {
                    this.ShowMessage("TY_M_UT_71NBQ558");
                    e.Successed = false;
                    return;
                }

                // 화물
                this.DbConnector.CommandClear();
                this.DbConnector.Attach(
                                       "TY_P_UT_668DS093",
                                       "HM",
                                       ds.Tables[1].Rows[i]["IJHWAMUL"].ToString()
                                       );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count <= 0)
                {
                    this.ShowMessage("TY_M_UT_71NBR559");
                    e.Successed = false;
                    return;
                }

                // 탱크번호
                this.DbConnector.CommandClear();
                this.DbConnector.Attach(
                                       "TY_P_UT_66SDH426",
                                       ds.Tables[1].Rows[i]["IJTANKNO"].ToString().Trim()
                                       );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count <= 0)
                {
                    this.ShowMessage("TY_M_UT_6AKBV437");
                    e.Successed = false;
                    return;
                }

                if (ds.Tables[1].Rows[i]["IJWKTYPE"].ToString() == "01")
                {
                    if (ds.Tables[1].Rows[i]["IJCARNO"].ToString() == "")
                    {
                        this.ShowMessage("TY_M_UT_7AHGM834");
                        e.Successed = false;
                        return;
                    }
                    else
                    {
                        // 차량번호
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach(
                                               "TY_P_UT_6BAH0732",
                                               ds.Tables[1].Rows[i]["IJCARNO"].ToString()
                                               );

                        dt = this.DbConnector.ExecuteDataTable();

                        if (dt.Rows.Count <= 0)
                        {
                            this.ShowMessage("TY_M_UT_71NBW561");
                            e.Successed = false;
                            return;
                        }
                    }
                }
                else
                {
                    //if (ds.Tables[1].Rows[i]["IJSEALNUM"].ToString() == "")
                    //{
                    //    this.ShowMessage("TY_M_UT_7AHHR836");
                    //    e.Successed = false;
                    //    return;
                    //}

                    if (ds.Tables[1].Rows[i]["IJCONTAIN"].ToString() == "")
                    {
                        this.ShowMessage("TY_M_UT_7AHGM833");
                        e.Successed = false;
                        return;
                    }
                    else
                    {
                        if (ds.Tables[1].Rows[i]["IJCONTAIN"].ToString().Length != 11)
                        {
                            this.ShowMessage("TY_M_UT_7AHHI835");
                            e.Successed = false;
                            return;
                        }

                        string sCONTNUM = string.Empty;

                        sCONTNUM = ds.Tables[1].Rows[i]["IJCONTAIN"].ToString();

                        for (int j = 0; j < 10; j++)
                        {
                            if (j == 0 || j == 1 || j == 2 || j == 3)
                            {
                                if (sCONTNUM.Substring(j, 1).ToString() != "A" &&
                                    sCONTNUM.Substring(j, 1).ToString() != "B" &&
                                    sCONTNUM.Substring(j, 1).ToString() != "C" &&
                                    sCONTNUM.Substring(j, 1).ToString() != "D" &&
                                    sCONTNUM.Substring(j, 1).ToString() != "E" &&
                                    sCONTNUM.Substring(j, 1).ToString() != "F" &&
                                    sCONTNUM.Substring(j, 1).ToString() != "G" &&
                                    sCONTNUM.Substring(j, 1).ToString() != "H" &&
                                    sCONTNUM.Substring(j, 1).ToString() != "I" &&
                                    sCONTNUM.Substring(j, 1).ToString() != "J" &&
                                    sCONTNUM.Substring(j, 1).ToString() != "K" &&
                                    sCONTNUM.Substring(j, 1).ToString() != "L" &&
                                    sCONTNUM.Substring(j, 1).ToString() != "M" &&
                                    sCONTNUM.Substring(j, 1).ToString() != "N" &&
                                    sCONTNUM.Substring(j, 1).ToString() != "O" &&
                                    sCONTNUM.Substring(j, 1).ToString() != "P" &&
                                    sCONTNUM.Substring(j, 1).ToString() != "Q" &&
                                    sCONTNUM.Substring(j, 1).ToString() != "R" &&
                                    sCONTNUM.Substring(j, 1).ToString() != "S" &&
                                    sCONTNUM.Substring(j, 1).ToString() != "T" &&
                                    sCONTNUM.Substring(j, 1).ToString() != "U" &&
                                    sCONTNUM.Substring(j, 1).ToString() != "V" &&
                                    sCONTNUM.Substring(j, 1).ToString() != "W" &&
                                    sCONTNUM.Substring(j, 1).ToString() != "X" &&
                                    sCONTNUM.Substring(j, 1).ToString() != "Y" &&
                                    sCONTNUM.Substring(j, 1).ToString() != "Z")
                                {
                                    this.ShowMessage("TY_M_UT_7AHGJ830");
                                    e.Successed = false;
                                    return;
                                }
                            }
                            else
                            {

                                if (sCONTNUM.Substring(j, 1).ToString() != "0" &&
                                    sCONTNUM.Substring(j, 1).ToString() != "1" &&
                                    sCONTNUM.Substring(j, 1).ToString() != "2" &&
                                    sCONTNUM.Substring(j, 1).ToString() != "3" &&
                                    sCONTNUM.Substring(j, 1).ToString() != "4" &&
                                    sCONTNUM.Substring(j, 1).ToString() != "5" &&
                                    sCONTNUM.Substring(j, 1).ToString() != "6" &&
                                    sCONTNUM.Substring(j, 1).ToString() != "7" &&
                                    sCONTNUM.Substring(j, 1).ToString() != "8" &&
                                    sCONTNUM.Substring(j, 1).ToString() != "9")
                                {
                                    this.ShowMessage("TY_M_UT_7AHGJ832");
                                    e.Successed = false;
                                    return;
                                }
                            }
                        }
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

        #region Description : 삭제 ProcessCheck
        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt1 = new DataTable();

            DataTable dt = this.FPS91_TY_S_UT_71JG1546.GetDataSourceInclude(TSpread.TActionType.Remove, "IJIPDATE", "IJTKNO");

            if (dt.Rows.Count == 0)
            {
                this.ShowMessage("TY_M_GB_23NAD870");
                e.Successed = false;
                return;
            }
            else
            {
                string sCJJISINO1 = string.Empty;

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    sCJJISINO1 = Get_Date(dt.Rows[i]["IJIPDATE"].ToString()) + Set_Fill3(dt.Rows[i]["IJTKNO"].ToString());

                    // 강제 계근 자료 체크
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_UT_71NDE562",
                        sCJJISINO1
                        );

                    dt1 = this.DbConnector.ExecuteDataTable();

                    if (dt1.Rows.Count > 0)
                    {
                        this.ShowMessage("TY_M_UT_71NDI563");

                        e.Successed = false;
                        return;
                    }
                }
            }

            if (!this.ShowMessage("TY_M_GB_23NAD872"))
            {
                e.Successed = false;
                return;
            }

            e.ArgData = dt;
        }
        #endregion

        #region Description : 스프레드 이벤트
        private void FPS91_TY_S_UT_71JG1546_RowInserted(object sender, TSpread.TAlterEventRow e)
        {
            //fsIJIPDATE = DateTime.Now.ToString("yyyyMMdd");

            //this.FPS91_TY_S_UT_71JG1546.SetValue(e.RowIndex, "IJIPDATE", fsIJIPDATE);

            if (this.FPS91_TY_S_UT_71JG1546.ActiveSheet.RowCount > 1)
            {
                if (fsIJIPDATE == "")
                {
                    fsIJIPDATE = DateTime.Now.ToString("yyyyMMdd");
                    this.FPS91_TY_S_UT_71JG1546.SetValue(e.RowIndex, "IJIPDATE", fsIJIPDATE);
                }
                else
                {
                    this.FPS91_TY_S_UT_71JG1546.SetValue(e.RowIndex, "IJIPDATE", fsIJIPDATE);
                }

                if (fsIJWKTYPE == "")
                {
                    this.FPS91_TY_S_UT_71JG1546.SetValue(e.RowIndex, "IJWKTYPE", "01");
                }
                else
                {
                    this.FPS91_TY_S_UT_71JG1546.SetValue(e.RowIndex, "IJWKTYPE", fsIJWKTYPE);
                }

                if (fsIJHWAJU == "")
                {
                    this.FPS91_TY_S_UT_71JG1546.SetValue(e.RowIndex, "IJHWAJU", "");
                }
                else
                {
                    this.FPS91_TY_S_UT_71JG1546.SetValue(e.RowIndex, "IJHWAJU", fsIJHWAJU);
                }

                if (fsHJDESC1 == "")
                {
                    this.FPS91_TY_S_UT_71JG1546.SetValue(e.RowIndex, "HJDESC1", "");
                }
                else
                {
                    this.FPS91_TY_S_UT_71JG1546.SetValue(e.RowIndex, "HJDESC1", fsHJDESC1);
                }

                if (fsIJHWAMUL == "")
                {
                    this.FPS91_TY_S_UT_71JG1546.SetValue(e.RowIndex, "IJHWAMUL", "");
                }
                else
                {
                    this.FPS91_TY_S_UT_71JG1546.SetValue(e.RowIndex, "IJHWAMUL", fsIJHWAMUL);
                }

                if (fsHMDESC1 == "")
                {
                    this.FPS91_TY_S_UT_71JG1546.SetValue(e.RowIndex, "HMDESC1", "");
                }
                else
                {
                    this.FPS91_TY_S_UT_71JG1546.SetValue(e.RowIndex, "HMDESC1", fsHMDESC1);
                }

                if (fsIJTANKNO == "")
                {
                    this.FPS91_TY_S_UT_71JG1546.SetValue(e.RowIndex, "IJTANKNO", "");
                }
                else
                {
                    this.FPS91_TY_S_UT_71JG1546.SetValue(e.RowIndex, "IJTANKNO", fsIJTANKNO);
                }

                if (fsIJIPQTY == "")
                {
                    this.FPS91_TY_S_UT_71JG1546.SetValue(e.RowIndex, "IJIPQTY", "0.000");
                }
                else
                {
                    this.FPS91_TY_S_UT_71JG1546.SetValue(e.RowIndex, "IJIPQTY", fsIJIPQTY);
                }

                if (fsIJCARNO == "")
                {
                    this.FPS91_TY_S_UT_71JG1546.SetValue(e.RowIndex, "IJCARNO", "");
                }
                else
                {
                    this.FPS91_TY_S_UT_71JG1546.SetValue(e.RowIndex, "IJCARNO", fsIJCARNO);
                }

                if (fsIJCONTAIN == "")
                {
                    this.FPS91_TY_S_UT_71JG1546.SetValue(e.RowIndex, "IJCONTAIN", "");
                }
                else
                {
                    this.FPS91_TY_S_UT_71JG1546.SetValue(e.RowIndex, "IJCONTAIN", fsIJCONTAIN);
                }

                if (fsIJSEALNUM == "")
                {
                    this.FPS91_TY_S_UT_71JG1546.SetValue(e.RowIndex, "IJSEALNUM", "");
                }
                else
                {
                    this.FPS91_TY_S_UT_71JG1546.SetValue(e.RowIndex, "IJSEALNUM", fsIJSEALNUM);
                }

                if (fsIJTMGUBN == "")
                {
                    this.FPS91_TY_S_UT_71JG1546.SetValue(e.RowIndex, "IJTMGUBN", "1");
                }
                else
                {
                    this.FPS91_TY_S_UT_71JG1546.SetValue(e.RowIndex, "IJTMGUBN", fsIJTMGUBN);
                }

                if (fsIJIPTIME1 == "")
                {
                    this.FPS91_TY_S_UT_71JG1546.SetValue(e.RowIndex, "IJIPTIME1", "");
                }
                else
                {
                    this.FPS91_TY_S_UT_71JG1546.SetValue(e.RowIndex, "IJIPTIME1", fsIJIPTIME1);
                }

                if (fsIJIPTIME2 == "")
                {
                    this.FPS91_TY_S_UT_71JG1546.SetValue(e.RowIndex, "IJIPTIME2", "");
                }
                else
                {
                    this.FPS91_TY_S_UT_71JG1546.SetValue(e.RowIndex, "IJIPTIME2", fsIJIPTIME2);
                }

                if (fsIJDESC == "")
                {
                    this.FPS91_TY_S_UT_71JG1546.SetValue(e.RowIndex, "IJDESC", "");
                }
                else
                {
                    this.FPS91_TY_S_UT_71JG1546.SetValue(e.RowIndex, "IJDESC", fsIJDESC);
                }
            }
            else
            {
                fsIJWKTYPE = "01";
                fsIJTMGUBN = "1";
                fsIJIPDATE = DateTime.Now.ToString("yyyyMMdd");
                this.FPS91_TY_S_UT_71JG1546.SetValue(e.RowIndex, "IJWKTYPE", fsIJWKTYPE);
                this.FPS91_TY_S_UT_71JG1546.SetValue(e.RowIndex, "IJTMGUBN", fsIJTMGUBN);
                this.FPS91_TY_S_UT_71JG1546.SetValue(e.RowIndex, "IJIPDATE", fsIJIPDATE);
            }
        }

        private void FPS91_TY_S_UT_71JG1546_Change(object sender, FarPoint.Win.Spread.ChangeEventArgs e)
        {
            if (e.Column == 0)
            {
                fsIJIPDATE = this.FPS91_TY_S_UT_71JG1546.GetValue("IJIPDATE").ToString();
            }

            if (e.Column == 2)
            {
                fsIJWKTYPE = this.FPS91_TY_S_UT_71JG1546.GetValue("IJWKTYPE").ToString();
            }

            if (e.Column == 3)
            {
                fsIJHWAJU = this.FPS91_TY_S_UT_71JG1546.GetValue("IJHWAJU").ToString();
            }

            if (e.Column == 4)
            {
                fsHJDESC1 = this.FPS91_TY_S_UT_71JG1546.GetValue("HJDESC1").ToString();
            }

            if (e.Column == 5)
            {
                fsIJHWAMUL = this.FPS91_TY_S_UT_71JG1546.GetValue("IJHWAMUL").ToString();
            }

            if (e.Column == 6)
            {
                fsHMDESC1 = this.FPS91_TY_S_UT_71JG1546.GetValue("HMDESC1").ToString();
            }

            if (e.Column == 7)
            {
                fsIJTANKNO = this.FPS91_TY_S_UT_71JG1546.GetValue("IJTANKNO").ToString();
            }

            if (e.Column == 8)
            {
                fsIJIPQTY = this.FPS91_TY_S_UT_71JG1546.GetValue("IJIPQTY").ToString();
            }

            if (e.Column == 10)
            {
                fsIJCARNO = this.FPS91_TY_S_UT_71JG1546.GetValue("IJCARNO").ToString();
            }

            if (e.Column == 11)
            {
                fsIJCONTAIN = this.FPS91_TY_S_UT_71JG1546.GetValue("IJCONTAIN").ToString();
            }

            if (e.Column == 12)
            {
                fsIJSEALNUM = this.FPS91_TY_S_UT_71JG1546.GetValue("IJSEALNUM").ToString();
            }

            if (e.Column == 13)
            {
                fsIJTMGUBN = this.FPS91_TY_S_UT_71JG1546.GetValue("IJTMGUBN").ToString();
            }

            if (e.Column == 14)
            {
                fsIJIPTIME1 = this.FPS91_TY_S_UT_71JG1546.GetValue("IJIPTIME1").ToString();

                if (this.FPS91_TY_S_UT_71JG1546.GetValue("IJIPDATE").ToString() != "" && this.FPS91_TY_S_UT_71JG1546.GetValue("IJIPTIME1").ToString() != "")
                {
                    this.FPS91_TY_S_UT_71JG1546_Sheet1.Cells[e.Row, 13].Value = UP_GetJITMGUBN(this.FPS91_TY_S_UT_71JG1546.GetValue("IJIPTIME1").ToString(),
                                                                                               this.FPS91_TY_S_UT_71JG1546.GetValue("IJIPDATE").ToString());
                }
            }

            if (e.Column == 15)
            {
                fsIJIPTIME2 = this.FPS91_TY_S_UT_71JG1546.GetValue("IJIPTIME2").ToString();
            }

            if (e.Column == 16)
            {
                fsIJDESC = this.FPS91_TY_S_UT_71JG1546.GetValue("IJDESC").ToString();
            }
        }
        #endregion

        #region Description : 차량번호 코드헬프
        private void FPS91_TY_S_UT_71JG1546_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == System.Windows.Forms.Keys.F1) && this.FPS91_TY_S_UT_71JG1546.ActiveSheet.ActiveColumnIndex == 10)
            {
                TYUTGB011S popup = new TYUTGB011S(this.FPS91_TY_S_UT_71JG1546.GetValue("IJCARNO").ToString());

                if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    this.FPS91_TY_S_UT_71JG1546.SetValue(this.FPS91_TY_S_UT_71JG1546.ActiveSheet.ActiveRowIndex, "IJCARNO", popup.fsCARNUMBER);

                    fsIJCARNO = popup.fsCARNUMBER;
                }

                if (this.FPS91_TY_S_UT_71JG1546.ActiveSheet.RowHeader.Cells[this.FPS91_TY_S_UT_71JG1546.ActiveSheet.ActiveRowIndex, 0].Text != "N")
                {
                    this.FPS91_TY_S_UT_71JG1546.ActiveSheet.RowHeader.Cells[this.FPS91_TY_S_UT_71JG1546.ActiveSheet.ActiveRowIndex, 0].Text = "U";
                }                
            }

            //fsIJWKTYPE = this.FPS91_TY_S_UT_71JG1546.GetValue("IJWKTYPE").ToString();
            //fsIJTMGUBN = this.FPS91_TY_S_UT_71JG1546.GetValue("IJTMGUBN").ToString();
        }
        #endregion

        #region Description : 지시 일괄 등록
        private void BTN61_UTTCODEHELP1_Click(object sender, EventArgs e)
        {
            if ((new TYUTGB023S()).ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
            }
        }
        #endregion

        #region Description : 입고지시 보류
        private void BTN61_UTTCODEHELP8_Click(object sender, EventArgs e)
        {
            if ((new TYUTGB025S()).ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
            }
        }
        #endregion

        #region Description : 특허구분 자동 선택
        private string UP_GetJITMGUBN(string sSTTIME, string sJIYYMM)
        {
            string sJITMGUBN = "1";
            string sGUBN = string.Empty;
            double dMMDD;
            double dDay;

            if (sSTTIME.Length == 2)
            {
                // 근태월력 조회 (1 : 토,일,공휴일 , 2 : 평일) 
                this.DbConnector.CommandClear();

                this.DbConnector.Attach
                    (
                    "TY_P_UT_A7OF1817",
                    Get_Date(sJIYYMM.ToString())
                    );

                DataTable dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    sGUBN = dt.Rows[0]["WEEK"].ToString();
                    dMMDD = Convert.ToDouble(Get_Date(sJIYYMM.ToString()).Substring(4, 4));
                    dDay = Convert.ToDouble(Get_Numeric(sSTTIME.ToString()));

                    if (dDay == 00)
                    {
                        dDay = 24;
                    }

                    if (sGUBN == "1")
                    {
                        if (dDay >= 09 && dDay < 24)   // 특허
                        {
                            sJITMGUBN = "3";
                        }
                        else // 조출
                        {
                            sJITMGUBN = "2";
                        }
                    }
                    else
                    {
                        if (dMMDD >= 0401 && dMMDD <= 1031) // 하절기
                        {
                            if (dDay >= 09 && dDay < 18)    // 일반
                            {
                                sJITMGUBN = "1";
                            }
                            else if (dDay >= 18 && dDay < 24)   // 특허
                            {
                                sJITMGUBN = "3";
                            }
                            else // 조출
                            {
                                sJITMGUBN = "2";
                            }
                        }
                        else // 동절기
                        {
                            if (dDay >= 09 && dDay < 17)    // 일반
                            {
                                sJITMGUBN = "1";
                            }
                            else if (dDay >= 17 && dDay < 24)   // 특허
                            {
                                sJITMGUBN = "3";
                            }
                            else // 조출
                            {
                                sJITMGUBN = "2";
                            }
                        }
                    }
                }
            }

            return sJITMGUBN;
        }
        #endregion
    }
}