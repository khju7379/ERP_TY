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

namespace TY.ER.US00
{
    /// <summary>
    /// 무인계근 차량 관리 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2016.10.05 14:53
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_UT_6A5D4295 : 무인계근 차량 조회
    ///  TY_P_UT_6A5DA296 : 무인계근 차량 등록
    ///  TY_P_UT_6A5DB297 : 무인계근 차량 수정
    ///  TY_P_UT_6A5DC298 : 무인계근 차량 삭제
    ///  TY_P_UT_6A5DD299 : 무인계근 차량 바코드 조회
    ///  TY_P_UT_6A5DF300 : 차량 중량파일 체크
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_US_96HDR867 : 무인계근 차량 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_246A2488 : 저장 작업을 실패했습니다.
    ///  TY_M_GB_23NAD871 : 저장하시겠습니까?
    ///  TY_M_GB_23NAD872 : 삭제하시겠습니까?
    ///  TY_M_GB_23NAD873 : 저장하였습니다.
    ///  TY_M_GB_23NAD874 : 삭제하였습니다.
    ///  TY_M_GB_43C9G671 : 삭제 작업을 실패했습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  INQ : 조회
    ///  NEW : 신규
    ///  REM : 삭제
    ///  SAV : 저장
    ///  TRHWAJU1 : 출고화주1
    ///  TRHWAJU2 : 출고화주2
    ///  TRHWAJU3 : 출고화주3
    ///  TRHWAMUL : 화물명
    ///  TRHYUNGT : 탱크로리
    ///  TRPUMM : 품목명
    ///  TRUNSONG : 차량소속
    ///  BARCODE : BAR-CODE
    ///  TRBALSU : 카드발급횟수
    ///  TRBIGO : 특기사항
    ///  TRCHJUSO : 차량주소
    ///  TRCHTEL : 차량전화
    ///  TRCOUNT : 유창개수
    ///  TRGIJUGO : 기사주소
    ///  TRGITEL : 기사전화
    ///  TRGUBUN : 구분
    ///  TRJUMIN1 : 주민번호1
    ///  TRJUMIN2 : 주민번호2
    ///  TRJUNGRY : 적재중량(MT)
    ///  TRMUMNO1 : 차량번호1
    ///  TRMUMNO2 : 차량번호2
    ///  TRTOTAL : 허가용량
    ///  TRUNNAME : 기사성명
    /// </summary>
    public partial class TYUSAU002I : TYBase
    {
        private string fsGUBUN = string.Empty;

        #region Description : 페이지 로드
        public TYUSAU002I()
        {
            InitializeComponent();
        }

        private void TYUSAU002I_Load(object sender, System.EventArgs e)
        {
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            this.BTN61_SAV_BATCH.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_BATCH_ProcessCheck);
            this.BTN61_BATCH.ProcessCheck += new TButton.CheckHandler(BTN61_BATCH_ProcessCheck);

            this.FPS91_TY_S_US_96HDR867.Initialize();

            BTN61_SAV.Visible = false;
            BTN61_REM.Visible = false;

            SetStartingFocus(this.TXT01_TRNUMN);

            UP_BTN_DISPLAY("");

            // DB 컨버젼 후 아래내용 삭제
            TXT01_TRBINNO.Visible = false;
            LBL51_TRBINNO.Visible = false;
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            string sProcedure = string.Empty;

            if (this.CBO01_GGUBUN.GetValue().ToString() == "A")      // 전체
            {
                sProcedure = "TY_P_US_96HDH865";
            }
            else if (this.CBO01_GGUBUN.GetValue().ToString() == "Y") // 발급
            {
                sProcedure = "TY_P_US_994DV185";
            }
            else // 미발급
            {
                sProcedure = "TY_P_US_994DV186";
            }

            this.FPS91_TY_S_US_96HDR867.Initialize();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                sProcedure.ToString(),
                this.TXT01_TRNUMN.GetValue().ToString(),
                this.TXT01_TRRFID_INQ.GetValue().ToString()
                );

            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.FPS91_TY_S_US_96HDR867.SetValue(dt);

                for (int i = 0; i < this.FPS91_TY_S_US_96HDR867.ActiveSheet.RowCount; i++)
                {
                    if (this.FPS91_TY_S_US_96HDR867.GetValue(i, "TRRFID").ToString() != "")
                    {
                        this.FPS91_TY_S_US_96HDR867_Sheet1.Cells[i, 3].Locked = true;
                        this.FPS91_TY_S_US_96HDR867.ActiveSheet.Cells[i, 4].ForeColor = Color.Blue;
                    }
                    else
                    {
                        this.FPS91_TY_S_US_96HDR867_Sheet1.Cells[i, 3].Locked = false;
                        this.FPS91_TY_S_US_96HDR867.ActiveSheet.Cells[i, 4].ForeColor = Color.Red;
                    }
                }
            }

            UP_FieldClear();
            UP_BTN_DISPLAY("");
        }
        #endregion

        #region Description : 신규 버튼
        private void BTN61_NEW_Click(object sender, EventArgs e)
        {
            fsGUBUN = "NEW";

            BTN61_SAV.Visible = true;
            BTN61_REM.Visible = false;

            UP_BTN_DISPLAY(fsGUBUN);

            UP_FieldClear();

            this.TXT01_TRNUMNO2.SetReadOnly(false);
            this.TXT01_TRNUMNO1.SetReadOnly(false);

            this.SetFocus(this.TXT01_TRNUMNO1);
        }
        #endregion

        #region Description : 저장 버튼
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            try
            {
                if (fsGUBUN == "NEW")
                {
                    //// 차량번호 읽기
                    //UP_SET_TRNUMNO1();

                    this.DbConnector.CommandClear();
                    //this.DbConnector.Attach("TY_P_US_9B8H4494", this.TXT01_TRNUMNO2.GetValue().ToString(),
                    this.DbConnector.Attach("TY_P_US_96HGF874", this.TXT01_TRNUMNO2.GetValue().ToString(),
                                                                this.TXT01_TRNUMNO1.GetValue().ToString(),
                                                                this.CBH01_TRUNSONG.GetValue().ToString(),
                                                                this.CBH01_TRHYUNGT.GetValue().ToString(),
                                                                this.TXT01_TRCHJUSO.GetValue().ToString(),
                                                                this.TXT01_TRCHTEL.GetValue().ToString(),
                                                                this.TXT01_TRUNNAME.GetValue().ToString(),
                                                                this.TXT01_TRJUMIN1.GetValue().ToString(),
                                                                this.TXT01_TRJUMIN2.GetValue().ToString(),
                                                                this.TXT01_TRGIJUSO.GetValue().ToString(),
                                                                this.TXT01_TRGITEL.GetValue().ToString(),
                                                                this.CBH01_TRHWAJU1.GetValue().ToString(),
                                                                this.CBH01_TRHWAJU2.GetValue().ToString(),
                                                                this.CBH01_TRHWAJU3.GetValue().ToString(),
                                                                this.TXT01_TRBALSU.GetValue().ToString(),
                                                                this.TXT01_TRBIGO.GetValue().ToString(),
                                                                this.CBO01_TRGUBUN.GetValue().ToString(),
                                                                this.TXT01_TRCOUNT.GetValue().ToString(),
                                                                // this.TXT01_TRBINNO.GetValue().ToString(),
                                                                this.TXT01_TRRFID.GetValue().ToString()
                                                                );
                    this.DbConnector.ExecuteNonQuery();
                }
                else
                {
                    this.DbConnector.CommandClear();
                    //this.DbConnector.Attach("TY_P_US_9B7FF484", this.CBH01_TRUNSONG.GetValue().ToString(),
                    this.DbConnector.Attach("TY_P_US_96HGH875", this.CBH01_TRUNSONG.GetValue().ToString(),
                                                                this.CBH01_TRHYUNGT.GetValue().ToString(),
                                                                this.TXT01_TRCHJUSO.GetValue().ToString(),
                                                                this.TXT01_TRCHTEL.GetValue().ToString(),
                                                                this.TXT01_TRUNNAME.GetValue().ToString(),
                                                                this.TXT01_TRJUMIN1.GetValue().ToString(),
                                                                this.TXT01_TRJUMIN2.GetValue().ToString(),
                                                                this.TXT01_TRGIJUSO.GetValue().ToString(),
                                                                this.TXT01_TRGITEL.GetValue().ToString(),
                                                                this.CBH01_TRHWAJU1.GetValue().ToString(),
                                                                this.CBH01_TRHWAJU2.GetValue().ToString(),
                                                                this.CBH01_TRHWAJU3.GetValue().ToString(),
                                                                this.TXT01_TRBALSU.GetValue().ToString(),
                                                                this.TXT01_TRBIGO.GetValue().ToString(),
                                                                this.CBO01_TRGUBUN.GetValue().ToString(),
                                                                this.TXT01_TRCOUNT.GetValue().ToString(),
                                                                // this.TXT01_TRBINNO.GetValue().ToString(),
                                                                this.TXT01_TRRFID.GetValue().ToString(),
                                                                this.TXT01_TRNUMNO2.GetValue().ToString(),
                                                                this.TXT01_TRNUMNO1.GetValue().ToString()
                                                                );
                    this.DbConnector.ExecuteNonQuery();
                }

                fsGUBUN = "UPT";
                UP_BTN_DISPLAY(fsGUBUN);

                this.ShowMessage("TY_M_GB_23NAD873");

                UP_RUN();
                this.BTN61_INQ_Click(null, null);
            }
            catch
            {
                this.ShowMessage("TY_M_AC_246A2488");
            }
        }
        #endregion

        #region Description : 삭제 버튼
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_US_96HFS873", this.TXT01_TRNUMNO2.GetValue().ToString(),
                                                        this.TXT01_TRNUMNO1.GetValue().ToString());

            this.DbConnector.ExecuteNonQuery();

            this.ShowMessage("TY_M_GB_23NAD874");

            UP_FieldClear();
            UP_BTN_DISPLAY("");

            BTN61_INQ_Click(null, null);
        }
        #endregion

        //#region Description : RF-ID 발급 버튼
        //private void BTN61_SAV_BATCH_Click(object sender, EventArgs e)
        //{
        //    string sTRNUMNO2 = string.Empty;
        //    string sTRNUMNO1 = string.Empty;
        //    string sTRRFID   = string.Empty;

        //    DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

        //    // RF-ID 발급
        //    this.DbConnector.CommandClear();
        //    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        //    {
        //        sTRNUMNO2 = ds.Tables[0].Rows[i]["TRNUMNO2"].ToString();
        //        sTRNUMNO1 = ds.Tables[0].Rows[i]["TRNUMNO1"].ToString();
        //        sTRRFID   = ds.Tables[0].Rows[i]["TRRFID"].ToString();

        //        this.DbConnector.Attach("TY_P_US_9968T201", sTRRFID.ToString(),
        //                                                    sTRNUMNO2.ToString(),
        //                                                    sTRNUMNO1.ToString());
        //    }
        //    this.DbConnector.ExecuteNonQueryList();

        //    this.ShowMessage("TY_M_US_9968U202");

        //    UP_FieldClear();
        //    UP_BTN_DISPLAY("");

        //    BTN61_INQ_Click(null, null);
        //}
        //#endregion

        #region Description : RF-ID 발급 갱신 버튼
        private void BTN61_BATCH_Click(object sender, EventArgs e)
        {
            string sTRNUMNO2 = string.Empty;
            string sTRNUMNO1 = string.Empty;
            string sTRRFID   = string.Empty;

            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            // RF-ID 발급 취소
            this.DbConnector.CommandClear();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                sTRNUMNO2 = ds.Tables[0].Rows[i]["TRNUMNO2"].ToString();
                sTRNUMNO1 = ds.Tables[0].Rows[i]["TRNUMNO1"].ToString();

                if (ds.Tables[0].Rows[i]["GUBUN"].ToString() == "발급")
                {
                    sTRRFID = "";
                }
                else
                {
                    sTRRFID = ds.Tables[0].Rows[i]["TRRFID"].ToString();
                }

                this.DbConnector.Attach("TY_P_US_9968T201", sTRRFID.ToString(),
                                                            sTRNUMNO2.ToString(),
                                                            sTRNUMNO1.ToString());
            }
            this.DbConnector.ExecuteNonQueryList();

            this.ShowMessage("TY_M_US_99NFF253");

            UP_FieldClear();
            UP_BTN_DISPLAY("");

            BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 적재중량 관리 버튼
        private void BTN61_SILOCODEHELP03_Click(object sender, EventArgs e)
        {
            //// 차량번호 읽기
            //UP_SET_TRNUMNO1();

            if (this.TXT01_TRNUMNO1.GetValue().ToString() == "" || this.TXT01_TRNUMNO2.GetValue().ToString() == "")
            {
                this.ShowMessage("TY_M_US_96HHH879");

                SetFocus(this.TXT01_TRNUMNO1);
            }
            else
            {
                if (this.OpenModalPopup(new TYUSAU02C1(this.TXT01_TRNUMNO1.GetValue().ToString(), this.TXT01_TRNUMNO2.GetValue().ToString())) == System.Windows.Forms.DialogResult.OK)
                    this.BTN61_INQ_Click(null, null);
            }
        }
        #endregion

        #region Description : RF-ID 등록
        private void BTN61_SILOCODEHELP06_Click(object sender, EventArgs e)
        {
            if (this.TXT01_TRNUMNO1.GetValue().ToString() == "" || this.TXT01_TRNUMNO2.GetValue().ToString() == "")
            {
                this.ShowMessage("TY_M_US_992EQ177");

                SetFocus(this.TXT01_TRNUMNO1);
            }
            else
            {
                DataTable dt = new DataTable();

                // 차량관리-공차차량 체크
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_US_99OEA257", Get_Date(DateTime.Now.ToString("yyyy-MM-dd")),
                                                            this.TXT01_TRNUMNO1.GetValue().ToString().Trim(),
                                                            this.TXT01_TRNUMNO2.GetValue().ToString().Trim());

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.ShowMessage("TY_M_US_99OEB258");
                    SetFocus(this.TXT01_TRRFID);

                    return;
                }

                if (this.BTN61_SILOCODEHELP06.Text == "RF-ID 발급")
                {
                    this.TXT01_TRRFID.SetReadOnly(false);
                }
                else
                {
                    this.TXT01_TRRFID.SetReadOnly(true);
                }

                this.TXT01_TRRFID.SetValue("");

                SetFocus(this.TXT01_TRRFID);
            }
        }
        #endregion

        #region Description : 확인 메소드
        private void UP_RUN()
        {
            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_US_96HED871", this.TXT01_TRNUMNO2.GetValue().ToString(),
                                                        this.TXT01_TRNUMNO1.GetValue().ToString());

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.CurrentDataTableRowMapping(dt, "01");

                Timer tmr = new Timer();

                tmr.Tick += delegate
                {
                    tmr.Stop();
                    this.SetFocus(this.CBH01_TRUNSONG.CodeText);
                };

                tmr.Interval = 100;
                tmr.Start();

                fsGUBUN = "UPT";
                UP_BTN_DISPLAY(fsGUBUN);

                if (this.TXT01_TRRFID.GetValue().ToString() != "")
                {
                    this.BTN61_SILOCODEHELP06.Text = "RF-ID 발급취소";
                }
                else
                {
                    this.BTN61_SILOCODEHELP06.Text = "RF-ID 발급";
                }

                this.TXT01_TRNUMNO2.SetReadOnly(true);
                this.TXT01_TRNUMNO1.SetReadOnly(true);
            }
        }
        #endregion

        #region Description : 저장 체크
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt = new DataTable();

            if (this.TXT01_TRRFID.GetValue().ToString().Trim() != "")
            {
                string sTRNUM = string.Empty;

                sTRNUM = this.TXT01_TRNUMNO1.GetValue().ToString().Trim() + this.TXT01_TRNUMNO2.GetValue().ToString().Trim();

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_US_992EA175", sTRNUM.ToString(),
                                                            this.TXT01_TRRFID.GetValue().ToString().Trim());

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.ShowMessage("TY_M_US_992EB176");

                    SetFocus(this.TXT01_TRRFID);

                    e.Successed = false;
                    return;
                }

                if (this.TXT01_TRRFID.GetValue().ToString().Length != 10)
                {
                    this.ShowMessage("TY_M_US_995A4187");

                    SetFocus(this.TXT01_TRRFID);

                    e.Successed = false;
                    return;
                }
            }

            // BIN 번호 유효성 체크
            if (this.TXT01_TRBINNO.GetValue().ToString() != "")
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_US_98DJW109",
                                        this.TXT01_TRBINNO.GetValue().ToString());

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count <= 0)
                {
                    this.ShowCustomMessage("BIN 번호를 확인하세요.", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                    e.Successed = false;
                    this.TXT01_TRBINNO.Focus();
                    return;
                }
            }

            if (!this.ShowMessage("TY_M_GB_23NAD871"))
            {
                e.Successed = false;
                return;
            }
        }
        #endregion

        #region Description : 삭제 체크
        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_US_96HHB876", this.TXT01_TRNUMNO1.GetValue().ToString(),
                                                        this.TXT01_TRNUMNO2.GetValue().ToString());

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.ShowMessage("TY_M_US_96HHB878");

                SetFocus(this.CBH01_TRUNSONG.CodeText);

                e.Successed = false;
                return;
            }

            if (!this.ShowMessage("TY_M_GB_23NAD872"))
            {
                e.Successed = false;
                return;
            }
        }
        #endregion

        #region Description : RF-ID 발급 ProcessCheck 이벤트
        private void BTN61_SAV_BATCH_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataSet ds = new DataSet();
            ds.Tables.Add(this.FPS91_TY_S_US_96HDR867.GetDataSourceInclude(TSpread.TActionType.Select, "TRNUMNO1", "TRNUMNO2", "TRRFID"));


            if (ds.Tables[0].Rows.Count == 0)
            {
                this.ShowMessage("TY_M_AC_2CV43442");
                e.Successed = false;
                return;
            }

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (ds.Tables[0].Rows[i]["TRRFID"].ToString() == "")
                {
                    this.ShowMessage("TY_M_US_99NDO250");
                    e.Successed = false;
                    return;
                }
            }

            if (!this.ShowMessage("TY_M_US_99NDP251"))
            {
                e.Successed = false;
                return;
            }

            e.ArgData = ds;
        }
        #endregion

        #region Description : RF-ID 발급취소 ProcessCheck 이벤트
        private void BTN61_BATCH_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataSet ds = new DataSet();
            ds.Tables.Add(this.FPS91_TY_S_US_96HDR867.GetDataSourceInclude(TSpread.TActionType.Select, "TRNUMNO1", "TRNUMNO2", "TRRFID", "GUBUN"));

            if (ds.Tables[0].Rows.Count == 0)
            {
                this.ShowMessage("TY_M_AC_2CV43442");
                e.Successed = false;
                return;
            }

            DataTable dt = new DataTable();


            string sAGO_RFID = string.Empty;
            string sAFT_RFID = string.Empty;

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (ds.Tables[0].Rows[i]["GUBUN"].ToString() == "발급")
                {
                    if (ds.Tables[0].Rows[i]["TRRFID"].ToString() == "")
                    {
                        this.ShowMessage("TY_M_US_9968R200");
                        e.Successed = false;
                        return;
                    }
                }
                else
                {
                    if (ds.Tables[0].Rows[i]["TRRFID"].ToString() == "")
                    {
                        this.ShowMessage("TY_M_US_99NDO250");
                        e.Successed = false;
                        return;
                    }
                    else
                    {
                        if (ds.Tables[0].Rows[i]["TRRFID"].ToString().Trim().Trim().Length != 10)
                        {
                            this.ShowMessage("TY_M_US_99O9A255");
                            e.Successed = false;
                            return;
                        }
                        else
                        {
                            string sTRNUM = string.Empty;

                            sTRNUM = ds.Tables[0].Rows[i]["TRNUMNO1"].ToString().Trim() + ds.Tables[0].Rows[i]["TRNUMNO2"].ToString().Trim();

                            this.DbConnector.CommandClear();
                            this.DbConnector.Attach("TY_P_US_992EA175", sTRNUM.ToString(),
                                                                        ds.Tables[0].Rows[i]["TRRFID"].ToString().Trim().Trim());

                            dt = this.DbConnector.ExecuteDataTable();

                            if (dt.Rows.Count > 0)
                            {
                                this.ShowMessage("TY_M_US_992EB176");

                                SetFocus(this.TXT01_TRRFID);

                                e.Successed = false;
                                return;
                            }
                        }
                    }

                    sAGO_RFID = ds.Tables[0].Rows[i]["TRRFID"].ToString().Trim().Trim();

                    for (int j = i +1; j < ds.Tables[0].Rows.Count; j++)
                    {
                        sAFT_RFID = ds.Tables[0].Rows[j]["TRRFID"].ToString().Trim().Trim();

                        if (sAGO_RFID.ToString() == sAFT_RFID.ToString())
                        {
                            this.ShowMessage("TY_M_US_99OBK256");
                            e.Successed = false;
                            return;
                        }
                    }
                }

                // 차량관리-공차차량 체크
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_US_99OEA257", Get_Date(DateTime.Now.ToString("yyyy-MM-dd")),
                                                            ds.Tables[0].Rows[i]["TRNUMNO1"].ToString().Trim().Trim(),
                                                            ds.Tables[0].Rows[i]["TRNUMNO2"].ToString().Trim().Trim());

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.ShowMessage("TY_M_US_99OEB258");

                    SetFocus(this.TXT01_TRRFID);

                    e.Successed = false;
                    return;
                }
            }

            if (!this.ShowMessage("TY_M_US_99NFE252"))
            {
                e.Successed = false;
                return;
            }

            e.ArgData = ds;
        }
        #endregion
        
        #region Description : 그리드 이벤트
        private void FPS91_TY_S_US_96HDR867_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            this.TXT01_TRNUMNO2.SetValue(this.FPS91_TY_S_US_96HDR867.GetValue("TRNUMNO2").ToString());
            this.TXT01_TRNUMNO1.SetValue(this.FPS91_TY_S_US_96HDR867.GetValue("TRNUMNO1").ToString());

            UP_RUN();
        }

        private void FPS91_TY_S_US_96HDR867_Change(object sender, FarPoint.Win.Spread.ChangeEventArgs e)
        {
            if (e.Column == 3)
            {
                this.FPS91_TY_S_US_96HDR867.ActiveSheet.RowHeader.Cells[e.Row, 0].Value = true;
            }
        }
        #endregion

        #region Description : 필드 초기화
        private void UP_FieldClear()
        {
            this.TXT01_TRRFID.SetValue("");
            this.CBH01_TRUNSONG.SetValue("");
            this.CBH01_TRHYUNGT.SetValue("");
            this.TXT01_TRCHJUSO.SetValue("");
            this.TXT01_TRCHTEL.SetValue("");
            this.TXT01_TRUNNAME.SetValue("");
            this.TXT01_TRJUMIN1.SetValue("");
            this.TXT01_TRJUMIN2.SetValue("");
            this.TXT01_TRGIJUSO.SetValue("");
            this.TXT01_TRGITEL.SetValue("");
            this.CBH01_TRHWAJU1.SetValue("");
            this.CBH01_TRHWAJU2.SetValue("");
            this.CBH01_TRHWAJU3.SetValue("");
            this.TXT01_TRBALSU.SetValue("");
            this.TXT01_TRBIGO.SetValue("");
            this.TXT01_TRCOUNT.SetValue("");
            this.TXT01_TRBINNO.SetValue("");
        }
        #endregion

        #region Description : 엔터키 이벤트
        private void TXT01_TRCOUNT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                SetFocus(this.BTN61_SAV);
            }
        }

        private void CBO01_GGUBUN_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                this.SetFocus(this.BTN61_INQ);
            }
        }

        private void TXT01_TRRFID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                this.SetFocus(this.BTN61_SAV);
            }
        }
        #endregion

        #region Description : 페이지 로드 포커스
        void tmrPage_Tick(object sender, EventArgs e)
        {
            ((Timer)sender).Stop();
            this.TXT01_TRNUMNO2.Focus();
        }
        #endregion

        #region Description : 버튼 DISPLAY
        private void UP_BTN_DISPLAY(string sGUBUN)
        {
            this.BTN61_SILOCODEHELP06.Text = "RF-ID 발급";

            if (sGUBUN == "NEW")
            {
                this.BTN61_SAV.Visible = true;
                this.BTN61_REM.Visible = false;
            }
            else if (sGUBUN == "UPT")
            {
                this.BTN61_SAV.Visible = true;
                this.BTN61_REM.Visible = true;
            }
            else
            {
                this.BTN61_SAV.Visible = false;
                this.BTN61_REM.Visible = false;
            }

            this.TXT01_TRRFID.SetReadOnly(true);
        }
        #endregion

        #region Description : 차량번호
        private void UP_SET_TRNUMNO1()
        {
            if (this.TXT01_TRNUMNO1.GetValue().ToString() != "")
            {
                string sSTNUM = string.Empty;
                string sEDNUM = string.Empty;
                string sNUM = string.Empty;
                string sGUBUN = string.Empty;

                if (this.TXT01_TRNUMNO1.GetValue().ToString().Length >= 5)
                {
                    switch (this.TXT01_TRNUMNO1.GetValue().ToString().Substring(2, 1))
                    {
                        case "1":
                            sSTNUM = "＇１＇";
                            sGUBUN = "1";
                            break;
                        case "2":
                            sSTNUM = "＇２＇";
                            sGUBUN = "1";
                            break;
                        case "3":
                            sSTNUM = "＇３＇";
                            sGUBUN = "1";
                            break;
                        case "4":
                            sSTNUM = "＇４＇";
                            sGUBUN = "1";
                            break;
                        case "5":
                            sSTNUM = "＇５＇";
                            sGUBUN = "1";
                            break;
                        case "6":
                            sSTNUM = "＇６＇";
                            sGUBUN = "1";
                            break;
                        case "7":
                            sSTNUM = "＇７＇";
                            sGUBUN = "1";
                            break;
                        case "8":
                            sSTNUM = "＇８＇";
                            sGUBUN = "1";
                            break;
                        case "9":
                            sSTNUM = "＇９＇";
                            sGUBUN = "1";
                            break;
                        case "0":
                            sSTNUM = "＇０＇";
                            sGUBUN = "1";
                            break;
                    }
                    switch (this.TXT01_TRNUMNO1.GetValue().ToString().Substring(3, 1))
                    {
                        case "1":
                            sEDNUM = "＇１＇";
                            break;
                        case "2":
                            sEDNUM = "＇２＇";
                            break;
                        case "3":
                            sEDNUM = "＇３＇";
                            break;
                        case "4":
                            sEDNUM = "＇４＇";
                            break;
                        case "5":
                            sEDNUM = "＇５＇";
                            break;
                        case "6":
                            sEDNUM = "＇６＇";
                            break;
                        case "7":
                            sEDNUM = "＇７＇";
                            break;
                        case "8":
                            sEDNUM = "＇８＇";
                            break;
                        case "9":
                            sEDNUM = "＇９＇";
                            break;
                        case "0":
                            sEDNUM = "＇０＇";
                            break;
                    }
                }
                else
                {
                    switch (this.TXT01_TRNUMNO1.GetValue().ToString().Substring(0, 1))
                    {
                        case "1":
                            sGUBUN = "1";
                            break;
                        case "2":
                            sGUBUN = "1";
                            break;
                        case "3":
                            sGUBUN = "1";
                            break;
                        case "4":
                            sGUBUN = "1";
                            break;
                        case "5":
                            sGUBUN = "1";
                            break;
                        case "6":
                            sGUBUN = "1";
                            break;
                        case "7":
                            sGUBUN = "1";
                            break;
                        case "8":
                            sGUBUN = "1";
                            break;
                        case "9":
                            sGUBUN = "1";
                            break;
                        case "0":
                            sGUBUN = "1";
                            break;
                    }
                }

                if (sGUBUN == "")
                {
                    if (this.TXT01_TRNUMNO1.GetValue().ToString().Length >= 5)
                    {
                        sSTNUM = this.TXT01_TRNUMNO1.GetValue().ToString().Substring(2, 1);
                        sEDNUM = this.TXT01_TRNUMNO1.GetValue().ToString().Substring(3, 1);
                    }
                    else
                    {
                        sSTNUM = this.TXT01_TRNUMNO1.GetValue().ToString().Substring(0, 1);
                        sEDNUM = this.TXT01_TRNUMNO1.GetValue().ToString().Substring(1, 1);
                    }
                }
                else
                {
                    if (this.TXT01_TRNUMNO1.GetValue().ToString().Length >= 5)
                    {
                        sSTNUM = sSTNUM.Substring(1, 1).ToString();
                        sEDNUM = sEDNUM.Substring(1, 1).ToString();
                    }
                }

                if (this.TXT01_TRNUMNO1.GetValue().ToString().Length >= 5)
                {
                    sNUM = this.TXT01_TRNUMNO1.GetValue().ToString().Substring(0, 2) + sSTNUM + sEDNUM + this.TXT01_TRNUMNO1.GetValue().ToString().Substring(4, 1);
                }
                else
                {
                    sNUM = this.TXT01_TRNUMNO1.GetValue().ToString();
                }
                this.TXT01_TRNUMNO1.SetValue(sNUM.ToString());
            }
        }
        #endregion

        #region Description : RF-ID 이벤트
        private void TXT01_TRRFID_TextChanged(object sender, EventArgs e)
        {
            if (this.TXT01_TRRFID.GetValue().ToString().Length == 10)
            {
                this.TXT01_TRRFID.SetReadOnly(true);
            }
        }
        #endregion
    }
}
