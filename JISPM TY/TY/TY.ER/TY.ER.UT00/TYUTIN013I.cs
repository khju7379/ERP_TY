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
using GrapeCity.ActiveReports;
using Shoveling2010.SmartClient.SystemUtility.Controls.FpSpreadCellType;
using FarPoint.Win.Spread.CellType;

namespace TY.ER.UT00
{
    public partial class TYUTIN013I : TYBase
    {
        private string fsPOPUP = string.Empty;

        private string fsJIIPHANG = string.Empty;
        private string fsJIBONSUN = string.Empty;
        private string fsJIHWAJU = string.Empty;
        private string fsJIHWAMUL = string.Empty;
        private string fsJIBLNO = string.Empty;
        private string fsJIMSNSEQ = string.Empty;
        private string fsJIHSNSEQ = string.Empty;
        private string fsJICUSTIL = string.Empty;
        private string fsJICHASU = string.Empty;
        private string fsJIACTHJ = string.Empty;
        private string fsJIJGHWAJU = string.Empty;
        private string fsJIYSHWAJU = string.Empty;
        private string fsJIYDHWAJU = string.Empty;
        private string fsJIYSDATE = string.Empty;
        private string fsJIYDSEQ = string.Empty;
        private string fsJIYSSEQ = string.Empty;
        private string fsJITANKNO = string.Empty;
        private string fsJIIPTANK = string.Empty;
        private string fsJICARNO2 = string.Empty;
        private string fsJICHHJ = string.Empty;

        private string fsGUBUN = string.Empty;
        private string fsRunCheck = string.Empty;

        #region Description : 페이지 로드

        public TYUTIN013I(string sJIYYMM, string sJISEQ)
        {
            InitializeComponent();

            this.DTP01_JIYYMM.SetValue(Set_Date(sJIYYMM.ToString()));
            this.TXT01_JISEQ.SetValue(sJISEQ.ToString());


            fsPOPUP = "POPUP";
        }

        public TYUTIN013I()
        {
            InitializeComponent();

            fsPOPUP = "";
        }

        private void TYUTIN013I_Load(object sender, System.EventArgs e)
        {
            this.FPS91_TY_S_UT_6BAD7710.Initialize();

            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            this.BTN61_BATCH.ProcessCheck += new TButton.CheckHandler(BTN61_BATCH_ProcessCheck);


            UP_BUTTON_Visible("");
            this.TXT01_JIGSPINO.SetReadOnly(true);
            this.SetStartingFocus(this.DTP01_STIPHANG);

            //로그인사번의 최근 2개월동안의 적요 셋팅
            UP_Get_MemoLogList();

            this.BTN61_INQ_Click(null, null);

            if (fsPOPUP == "POPUP")
            {
                // 확인
                UP_RUN();
            }
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            UP_SEARCH();
        }
        #endregion

        #region Description : 신규 버튼
        private void BTN61_NEW_Click(object sender, EventArgs e)
        {
            UP_Set_ReadOnly("NEW");

            UP_BUTTON_Visible("NEW");

            UP_FieldClear();

            fsGUBUN = "NEW";

            this.CBH01_JIHISAB.SetValue(TYUserInfo.EmpNo.ToString().Trim().ToUpper());

            SetFocus(this.TXT01_JIJGHWAJU);
        }
        #endregion

        #region Description : 저장 버튼
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            string sGUBUN = string.Empty;
            string sJCSEQ = string.Empty;

            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();

            if (fsGUBUN.ToString() == "NEW") // 신규
            {
                int i = 0;

                // 차량대수만큼 등록하기
                for (i = 0; i < int.Parse(this.CBO01_JIJICNT.GetValue().ToString()); i++)
                {
                    // 출고지시 번호 등록
                    UP_UTIJICNF_INS();

                    // 출고지시 등록
                    UP_UTIJISIF_INS();

                    // 대기차량관리 업데이트
                    UP_UTICARSTBF_UPDATE("NEW");
                }
            }
            else
            {
                // 출고지시 수정
                UP_UTIJISIF_UPT();
            }

            // 조회
            UP_SEARCH();

            UP_BUTTON_Visible("");

            this.ShowMessage("TY_M_GB_23NAD873");
        }
        #endregion

        #region Description : 삭제 버튼
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            // 출고지시 삭제
            UP_UTIJISIF_DEL();

            // 대기차량관리 업데이트
            UP_UTICARSTBF_UPDATE("");

            if (this.TXT01_ORDATE.GetValue().ToString() != "" && this.TXT01_ORSEQ.GetValue().ToString() != "")
            {
                // 고객정보시스템 오더파일의 지시번호 클리어
                UP_UTIORDERF_UPDATE();
            }

            // 조회
            UP_SEARCH();

            UP_BUTTON_Visible("");

            this.ShowMessage("TY_M_GB_23NAD874");
        }
        #endregion

        #region Description : 출고오더 조회 버튼
        private void BTN62_INQ_Click(object sender, EventArgs e)
        {
            string sVNRPCODE = string.Empty;

            if (this.CBH01_SHWAJU.GetValue().ToString() != "")
            {
                // 대표거래처 코드 가져오기
                sVNRPCODE = Get_VNCODE(this.CBH01_SHWAJU.GetValue().ToString());
            }

            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();

            if (this.CBH01_SHWAJU.GetValue().ToString() == "")
            {
                this.DbConnector.Attach
                   (
                   "TY_P_UT_77KDI250",
                   Get_Date(this.DTP01_STDATE.GetValue().ToString()),
                   Get_Date(this.DTP01_EDDATE.GetValue().ToString())
                   );
            }
            else
            {
                this.DbConnector.Attach
                   (
                   "TY_P_UT_77KDI251",
                   Get_Date(this.DTP01_STDATE.GetValue().ToString()),
                   Get_Date(this.DTP01_EDDATE.GetValue().ToString()),
                   sVNRPCODE.ToString()
                   );
            }

            dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_UT_6BAD7710.SetValue(dt);

            if (this.FPS91_TY_S_UT_6BAD7710.CurrentRowCount > 0)
            {
                for (int i = 0; i < this.FPS91_TY_S_UT_6BAD7710.CurrentRowCount; i++)
                {
                    if (this.FPS91_TY_S_UT_6BAD7710.GetValue(i, "JIJOBGB").ToString() == "보류")
                    {
                        this.FPS91_TY_S_UT_6BAD7710_Sheet1.Cells[i, 10].Font = new Font("굴림", 9, FontStyle.Bold);
                        this.FPS91_TY_S_UT_6BAD7710_Sheet1.Cells[i, 10].ForeColor = Color.Red;
                    }
                }
            }
        }
        #endregion

        #region Description : 출고오더 처리 버튼
        private void BTN61_BATCH_Click(object sender, EventArgs e)
        {
            string sJIJOBGB = string.Empty;

            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            this.DbConnector.CommandClear();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (ds.Tables[0].Rows[i]["JIJOBGB"].ToString() == "보류")
                {
                    sJIJOBGB = "";
                }
                else
                {
                    sJIJOBGB = "H";
                }
                this.DbConnector.Attach("TY_P_UT_77KDB247", sJIJOBGB.ToString(),
                                                            ds.Tables[0].Rows[i]["JIYYMM"].ToString(),
                                                            ds.Tables[0].Rows[i]["JISEQ"].ToString());
            }

            this.DbConnector.ExecuteTranQueryList();

            this.ShowMessage("TY_M_MR_2BF50354");

            this.BTN62_INQ_Click(null, null);
        }
        #endregion

        #region Description : 처리 ProcessCheck 이벤트
        private void BTN61_BATCH_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataSet ds = new DataSet();
            ds.Tables.Add(this.FPS91_TY_S_UT_6BAD7710.GetDataSourceInclude(TSpread.TActionType.Select, "JIYYMM", "JISEQ", "JIJOBGB"));

            if (ds.Tables[0].Rows.Count == 0)
            {
                this.ShowMessage("TY_M_AC_2CV43442");
                e.Successed = false;
                return;
            }

            if (!this.ShowMessage("TY_M_MR_2BF50353"))
            {
                e.Successed = false;
                return;
            }

            e.ArgData = ds;
        }
        #endregion

        #region Description : 출고지시번호 등록
        private void UP_UTIJICNF_INS()
        {
            string sJCSEQ = string.Empty;

            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();

            this.DbConnector.Attach
                (
                "TY_P_UT_6BBD7735",
                Get_Date(this.DTP01_JIYYMM.GetValue().ToString())
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count <= 0)
            {
                sJCSEQ = "1";

                // 출고지시번호 등록
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_6BBD4737", Get_Date(this.DTP01_JIYYMM.GetValue().ToString()),
                                                            sJCSEQ.ToString()
                                                            );

                this.DbConnector.ExecuteNonQuery();
            }
            else
            {
                this.DbConnector.Attach
                   (
                   "TY_P_UT_6BBD1736",
                   Get_Date(this.DTP01_JIYYMM.GetValue().ToString())
                   );

                dt1 = this.DbConnector.ExecuteDataTable();

                if (dt1.Rows.Count > 0)
                {
                    sJCSEQ = dt1.Rows[0]["JCSEQ"].ToString();
                }

                // 출고지시번호 수정
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_6BBD4738", sJCSEQ.ToString(),
                                                            Get_Date(this.DTP01_JIYYMM.GetValue().ToString())
                                                            );

                this.DbConnector.ExecuteNonQuery();
            }

            // 지시 순번
            this.TXT01_JISEQ.SetValue(sJCSEQ.ToString());
        }
        #endregion

        #region Description : 출고지시 등록
        private void UP_UTIJISIF_INS()
        {
            // 출고지시번호 등록
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_A7OGE818", Get_Date(this.DTP01_JIYYMM.GetValue().ToString()),
                                                        this.TXT01_JISEQ.GetValue().ToString(),
                                                        Get_Date(this.DTP01_JIIPHANG.GetValue().ToString()),
                                                        this.CBH01_JIBONSUN.GetValue().ToString(),
                                                        this.CBH01_JIHWAJU.GetValue().ToString(),
                                                        this.CBH01_JIHWAMUL.GetValue().ToString(),
                                                        this.TXT01_JIBLNO.GetValue().ToString(),
                                                        this.TXT01_JIMSNSEQ.GetValue().ToString(),
                                                        this.TXT01_JIHSNSEQ.GetValue().ToString(),
                                                        this.CBH01_JIACTHJ.GetValue().ToString(),
                                                        Get_Date(this.DTP01_JICUSTIL.GetValue().ToString()),
                                                        this.TXT01_JICHASU.GetValue().ToString(),
                                                        this.TXT01_JIJGHWAJU.GetValue().ToString(),
                                                        this.CBH01_JIYSHWAJU.GetValue().ToString(),
                                                        this.CBH01_JIYDHWAJU.GetValue().ToString(),
                                                        Get_Date(this.DTP01_JIYSDATE.GetValue().ToString()),
                                                        this.TXT01_JIYDSEQ.GetValue().ToString(),
                                                        this.TXT01_JIYSSEQ.GetValue().ToString(),
                                                        this.CBO01_JIYNGUBUN.GetValue().ToString().Replace("N", ""),
                                                        this.CBH01_JICHHJ.GetValue().ToString(),
                                                        Set_TankNo(this.TXT01_JITANKNO.GetValue().ToString()),
                                                        Set_TankNo(this.TXT01_JIIPTANK.GetValue().ToString()),
                                                        this.TXT01_JIJANQTY.GetValue().ToString(),
                                                        this.TXT01_JISTMTQTY.GetValue().ToString(),
                                                        this.TXT01_JIEDMTQTY.GetValue().ToString(),
                                                        this.TXT01_JISTLTQTY.GetValue().ToString(),
                                                        this.TXT01_JIEDLTQTY.GetValue().ToString(),
                                                        this.CBO01_JITMGUBN.GetValue().ToString(),
                                                        this.TXT01_JITIMEHH.GetValue().ToString(),
                                                        this.TXT01_JITIMEMM.GetValue().ToString(),
                                                        this.CBH01_JIDNST.GetValue().ToString(),
                                                        "1",                                         // 차량대수
                                                        this.TXT01_JICARNO1.GetValue().ToString(),
                                                        this.TXT01_JICARNO2.GetValue().ToString(),
                                                        this.CBH01_JICHJANG.GetValue().ToString(),
                                                        this.CBO01_JICHTYPE.GetValue().ToString(),
                                                        this.CBO01_JIUNIT.GetValue().ToString(),
                                                        this.CBO01_JIWKTYPE.GetValue().ToString(),
                                                        "1",   // RACK
                                                        "1",   // PUMP
                                                        this.TXT01_JICONTNUM.GetValue().ToString(),
                                                        this.TXT01_JISILNUM.GetValue().ToString(),
                                                        this.CBO01_JISILNUMCK.GetValue().ToString(),
                                                        this.TXT01_JIGSPINO.GetValue().ToString(),
                                                        TYUserInfo.EmpNo.ToString().Trim().ToUpper()
                                                        );

            this.DbConnector.ExecuteNonQuery();
        }
        #endregion

        #region Description : 출고지시 수정
        private void UP_UTIJISIF_UPT()
        {
            // 출고지시번호 수정
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_A7OGE819", Get_Date(this.DTP01_JIIPHANG.GetValue().ToString()),
                                                        this.CBH01_JIBONSUN.GetValue().ToString(),
                                                        this.CBH01_JIHWAJU.GetValue().ToString(),
                                                        this.CBH01_JIHWAMUL.GetValue().ToString(),
                                                        this.TXT01_JIBLNO.GetValue().ToString(),
                                                        this.TXT01_JIMSNSEQ.GetValue().ToString(),
                                                        this.TXT01_JIHSNSEQ.GetValue().ToString(),
                                                        this.CBH01_JIACTHJ.GetValue().ToString(),
                                                        Get_Date(this.DTP01_JICUSTIL.GetValue().ToString()),
                                                        this.TXT01_JICHASU.GetValue().ToString(),
                                                        this.TXT01_JIJGHWAJU.GetValue().ToString(),
                                                        this.CBH01_JIYSHWAJU.GetValue().ToString(),
                                                        this.CBH01_JIYDHWAJU.GetValue().ToString(),
                                                        Get_Date(this.DTP01_JIYSDATE.GetValue().ToString()),
                                                        this.TXT01_JIYDSEQ.GetValue().ToString(),
                                                        this.TXT01_JIYSSEQ.GetValue().ToString(),
                                                        this.CBO01_JIYNGUBUN.GetValue().ToString().Replace("N", ""),
                                                        this.CBH01_JICHHJ.GetValue().ToString(),
                                                        Set_TankNo(this.TXT01_JITANKNO.GetValue().ToString()),
                                                        Set_TankNo(this.TXT01_JIIPTANK.GetValue().ToString()),
                                                        this.TXT01_JIJANQTY.GetValue().ToString(),
                                                        this.TXT01_JISTMTQTY.GetValue().ToString(),
                                                        this.TXT01_JIEDMTQTY.GetValue().ToString(),
                                                        this.TXT01_JISTLTQTY.GetValue().ToString(),
                                                        this.TXT01_JIEDLTQTY.GetValue().ToString(),
                                                        this.CBO01_JITMGUBN.GetValue().ToString(),
                                                        this.TXT01_JITIMEHH.GetValue().ToString(),
                                                        this.TXT01_JITIMEMM.GetValue().ToString(),
                                                        this.CBH01_JIDNST.GetValue().ToString(),
                                                        this.CBO01_JIJICNT.GetValue().ToString(),
                                                        this.TXT01_JICARNO1.GetValue().ToString(),
                                                        this.TXT01_JICARNO2.GetValue().ToString(),
                                                        this.CBH01_JICHJANG.GetValue().ToString(),
                                                        this.CBO01_JICHTYPE.GetValue().ToString(),
                                                        this.CBO01_JIUNIT.GetValue().ToString(),
                                                        this.CBO01_JIWKTYPE.GetValue().ToString(),
                                                        "1",   // RACK
                                                        "1",   // PUMP
                                                        this.TXT01_JICONTNUM.GetValue().ToString(),
                                                        this.TXT01_JISILNUM.GetValue().ToString(),
                                                        this.CBO01_JISILNUMCK.GetValue().ToString(),
                                                        this.TXT01_JIGSPINO.GetValue().ToString(),
                                                        TYUserInfo.EmpNo.ToString().Trim().ToUpper(),
                                                        Get_Date(this.DTP01_JIYYMM.GetValue().ToString()),
                                                        this.TXT01_JISEQ.GetValue().ToString()
                                                        );

            this.DbConnector.ExecuteNonQuery();
        }
        #endregion

        #region Description : 대기차량관리 업데이트
        private void UP_UTICARSTBF_UPDATE(string sGUBUN)
        {
            string sSTJISINUM = string.Empty;
            string sSTJISITIME = string.Empty;

            if (sGUBUN == "NEW") // 등록
            {
                sSTJISINUM = Get_Date(this.DTP01_JIYYMM.GetValue().ToString());
                sSTJISITIME = DateTime.Now.ToString("HHmmss").ToString();
            }
            else // 삭제
            {
                sSTJISINUM = "";
                sSTJISITIME = "0";
            }

            // 대기차량관리 업데이트
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_6BBEP748", sSTJISINUM.ToString(),
                                                        sSTJISITIME.ToString(),
                                                        Get_Date(this.DTP01_JIYYMM.GetValue().ToString()),
                                                        this.TXT01_JICARNO2.GetValue().ToString()
                                                        );

            this.DbConnector.ExecuteNonQuery();
        }
        #endregion

        #region Description : 출고지시 삭제
        private void UP_UTIJISIF_DEL()
        {
            // 출고지시번호 삭제
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_6BBE1741", Get_Date(this.DTP01_JIYYMM.GetValue().ToString()),
                                                        this.TXT01_JISEQ.GetValue().ToString()
                                                        );

            this.DbConnector.ExecuteNonQuery();
        }
        #endregion

        #region Description : 출고오더의 지시번호 클리어
        private void UP_UTIORDERF_UPDATE()
        {
            // 출고지시번호 삭제
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_7CJG0332", "0",
                                                        "0",
                                                        Get_Date(this.TXT01_ORDATE.GetValue().ToString()),
                                                        this.TXT01_ORSEQ.GetValue().ToString(),
                                                        this.TXT01_JIJGHWAJU.GetValue().ToString()
                                                        );

            this.DbConnector.ExecuteNonQuery();
        }
        #endregion

        #region Description : 조회 메소드
        private void UP_SEARCH()
        {
            string sProcId = "";

            DataTable dt = new DataTable();

            // 차량번호필드 입력문자 길이가 4이하인 경우 차량번호 검색
            // 4자리를 초과한 경우 컨테이너번호 검색
            if (this.TXT01_CHCARNO.GetValue().ToString().Length <= 4)
            {
                sProcId = "TY_P_UT_83DD5695";
            }
            else
            {
                sProcId = "TY_P_UT_A5DEZ416";
            }

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                sProcId,
                this.TXT01_JISEQ1.GetValue().ToString(),
                Get_Date(this.DTP01_STIPHANG.GetValue().ToString()),
                Get_Date(this.DTP01_EDIPHANG.GetValue().ToString()),
                this.TXT01_CHCARNO.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.FPS91_TY_S_UT_6AJJ1427.SetValue(dt);
            }
        }
        #endregion

        #region Description : 확인 메소드
        private void UP_RUN()
        {
            UP_Set_ReadOnly("OK");

            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_6AJJM429",
                Get_Date(this.DTP01_JIYYMM.GetValue().ToString()), // 입항일자
                this.TXT01_JISEQ.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                fsRunCheck = "RUN";
                this.CurrentDataTableRowMapping(dt, "01");
                if (this.TXT01_JITIMEHH.GetValue().ToString().Trim() == "")
                {
                    fsRunCheck = "";
                }
                // 체크 필드값 가져오기
                UP_Get_CheckField(dt.Rows[0]["JIIPHANG"].ToString(),
                                  dt.Rows[0]["JIBONSUN"].ToString(),
                                  dt.Rows[0]["JIHWAJU"].ToString(),
                                  dt.Rows[0]["JIHWAMUL"].ToString(),
                                  dt.Rows[0]["JIBLNO"].ToString(),
                                  dt.Rows[0]["JIMSNSEQ"].ToString(),
                                  dt.Rows[0]["JIHSNSEQ"].ToString(),
                                  dt.Rows[0]["JICUSTIL"].ToString(),
                                  dt.Rows[0]["JICHASU"].ToString(),
                                  dt.Rows[0]["JIACTHJ"].ToString(),
                                  dt.Rows[0]["JIJGHWAJU"].ToString(),
                                  dt.Rows[0]["JIYSHWAJU"].ToString(),
                                  dt.Rows[0]["JIYDHWAJU"].ToString(),
                                  dt.Rows[0]["JIYSDATE"].ToString(),
                                  dt.Rows[0]["JIYDSEQ"].ToString(),
                                  dt.Rows[0]["JIYSSEQ"].ToString(),
                                  dt.Rows[0]["JITANKNO"].ToString(),
                                  dt.Rows[0]["JIIPTANK"].ToString(),
                                  dt.Rows[0]["JICARNO2"].ToString(),
                                  dt.Rows[0]["JICHHJ"].ToString());

                // 출고지시 - 지시총량 가져오기
                UP_Get_SumJiqty();

                // 이전 HSN에 대한 데이터 가져오기
                UP_GET_PreIPMTQTY();

                fsGUBUN = "UPT";


                UP_BUTTON_Visible(fsGUBUN);
            }
        }
        #endregion

        #region Description : 체크 필드값 가져오기
        private void UP_Get_CheckField(string sJIIPHANG, string sJIBONSUN, string sJIHWAJU, string sJIHWAMUL, string sJIBLNO,
                                       string sJIMSNSEQ, string sJIHSNSEQ, string sJICUSTIL, string sJICHASU, string sJIACTHJ,
                                       string sJIJGHWAJU, string sJIYSHWAJU, string sJIYDHWAJU, string sJIYSDATE, string sJIYDSEQ,
                                       string sJIYSSEQ, string sJITANKNO, string sJIIPTANK, string sJICARNO2, string sJICHHJ)
        {
            fsJIIPHANG = "";
            fsJIBONSUN = "";
            fsJIHWAJU = "";
            fsJIHWAMUL = "";
            fsJIBLNO = "";
            fsJIMSNSEQ = "";
            fsJIHSNSEQ = "";
            fsJICUSTIL = "";
            fsJICHASU = "";
            fsJIACTHJ = "";
            fsJIJGHWAJU = "";
            fsJIYSHWAJU = "";
            fsJIYDHWAJU = "";
            fsJIYSDATE = "";
            fsJIYDSEQ = "";
            fsJIYSSEQ = "";
            fsJITANKNO = "";
            fsJIIPTANK = "";
            fsJICARNO2 = "";
            fsJICHHJ = "";

            fsJIIPHANG = sJIIPHANG.ToString();
            fsJIBONSUN = sJIBONSUN.ToString();
            fsJIHWAJU = sJIHWAJU.ToString();
            fsJIHWAMUL = sJIHWAMUL.ToString();
            fsJIBLNO = sJIBLNO.ToString();
            fsJIMSNSEQ = sJIMSNSEQ.ToString();
            fsJIHSNSEQ = sJIHSNSEQ.ToString();
            fsJICUSTIL = sJICUSTIL.ToString();
            fsJICHASU = sJICHASU.ToString();
            fsJIACTHJ = sJIACTHJ.ToString();
            fsJIJGHWAJU = sJIJGHWAJU.ToString();
            fsJIYSHWAJU = sJIYSHWAJU.ToString();
            fsJIYDHWAJU = sJIYDHWAJU.ToString();
            fsJIYSDATE = sJIYSDATE.ToString();
            fsJIYDSEQ = sJIYDSEQ.ToString();
            fsJIYSSEQ = sJIYSSEQ.ToString();
            fsJITANKNO = sJITANKNO.ToString();
            fsJIIPTANK = sJIIPTANK.ToString();
            fsJICARNO2 = sJICARNO2.ToString();
            fsJICHHJ = sJICHHJ.ToString();
        }
        #endregion

        #region Description : 출고지시 - 지시총량 가져오기
        private void UP_Get_SumJiqty()
        {
            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_6AJJS430",
                Get_Date(this.DTP01_JIYYMM.GetValue().ToString()), // 입항일자
                this.TXT01_JISEQ.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.TXT01_SUMCHQTY.SetValue(dt.Rows[0]["JICHQTY"].ToString());
            }
        }
        #endregion

        #region Description : 재고 조회(통관 및 양수도)
        private void UP_CALL_JEGO()
        {
            TYUTGB008S popup = new TYUTGB008S(this.TXT01_JIJGHWAJU.GetValue().ToString());

            if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.TXT01_JIJGHWAJU.SetValue(popup.fsJGHWAJU);     // 재고화주
                this.TXT01_JIJGHWAJUNM.SetValue(popup.fsJGHWAJUNM); // 재고화주명
                this.DTP01_JIIPHANG.SetValue(popup.fsIPHANG);       // 입항일자
                this.CBH01_JIBONSUN.SetValue(popup.fsBONSUN);       // 본선
                this.CBH01_JIHWAJU.SetValue(popup.fsHWAJU);         // 화주
                this.CBH01_JIHWAMUL.SetValue(popup.fsHWAMUL);       // 화물
                this.TXT01_JIBLNO.SetValue(popup.fsBLNO);           // BL번호
                this.TXT01_JIMSNSEQ.SetValue(popup.fsMSNSEQ);       // MSN번호
                this.TXT01_JIHSNSEQ.SetValue(popup.fsHSNSEQ);       // HSN번호
                this.DTP01_JICUSTIL.SetValue(popup.fsCUSTIL);       // 통관일자
                this.TXT01_JICHASU.SetValue(popup.fsCHASU);         // 통관차수
                this.CBH01_JIACTHJ.SetValue(popup.fsACTHJ);         // 통관화주
                this.CBH01_JIYSHWAJU.SetValue(popup.fsYSHWAJU);     // 양수화주
                this.CBH01_JIYDHWAJU.SetValue(popup.fsYDHWAJU);     // 양도화주
                this.DTP01_JIYSDATE.SetValue(popup.fsYSDATE);       // 양수일자
                this.TXT01_JIYDSEQ.SetValue(popup.fsYDSEQ);         // 양도순번
                this.TXT01_JIYSSEQ.SetValue(popup.fsYSSEQ);         // 양수순번
                this.CBO01_JIYNGUBUN.SetValue(popup.fsYNGUBUN);     // 양수구분

                this.TXT01_CJCUQTY.SetValue(popup.fsCJCUQTY);       // 통관수량
                this.TXT01_CJCHQTY.SetValue(popup.fsCJCHQTY);       // 출고수량
                this.TXT01_CJJEQTY.SetValue(popup.fsCJJEQTY);       // 재고수량




                this.TXT01_JIIPQTY.SetValue(popup.fsIPMTQTY);       // 입고수량
                this.TXT01_IPPAQTY.SetValue(popup.fsIPPAQTY);       // 통관누계
                this.TXT01_IPCHQTY.SetValue(popup.fsIPCHQTY);       // 출고누계
                this.TXT01_JIJEQTY.SetValue(popup.fsIPJEQTY);       // 통관재고
                this.TXT01_JIJANQTY.SetValue(popup.fsIPJANQTY);     // 미통관재고

                //this.CBH01_JICHHJ.SetValue(popup.fsJGHWAJU);        // 출고화주


                this.TXT01_JITANKNO.SetValue(popup.fsTANKNO);       // 출고탱크

                // 출하장 가져오기
                UP_GET_JICHJANG();

                // 이전 HSN에 대한 데이터 가져오기
                UP_GET_PreIPMTQTY();

                SetFocus(this.TXT01_JIIPTANK);

                //SetFocus(this.TXT01_JIJGHWAJU);
            }
        }
        #endregion

        #region Description : 출고탱크 가져오기
        private void UP_GET_JITANKNO()
        {
            this.TXT01_JITANKNO.SetValue("");

            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_6BAF3715",
                this.DTP01_JIIPHANG.GetValue().ToString(),       // 입항일자
                this.CBH01_JIBONSUN.GetValue().ToString(),       // 본선
                this.CBH01_JIHWAJU.GetValue().ToString(),        // 화주
                this.CBH01_JIHWAMUL.GetValue().ToString()        // 화물
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                if (dt.Rows.Count == 1)
                {
                    this.TXT01_JITANKNO.SetValue(dt.Rows[0]["SVTANKNO"].ToString());

                    // 출하장 가져오기
                    UP_GET_JICHJANG();
                }
            }
        }
        #endregion

        #region Description : 입고탱크 가져오기
        private void UP_GET_JIIPTANK()
        {
            this.TXT01_JIIPTANK.SetValue("");

            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_6BAF3716",
                this.DTP01_JIIPHANG.GetValue().ToString(),       // 입항일자
                this.CBH01_JIBONSUN.GetValue().ToString(),       // 본선
                this.CBH01_JIHWAJU.GetValue().ToString(),        // 화주
                this.CBH01_JIHWAMUL.GetValue().ToString()        // 화물
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                if (dt.Rows.Count == 1)
                {
                    this.TXT01_JIIPTANK.SetValue(dt.Rows[0]["COTANKNO"].ToString());
                }
            }
        }
        #endregion

        #region Description : 출하장 가져오기
        private void UP_GET_JICHJANG()
        {
            this.CBH01_JICHJANG.SetValue("");

            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_6BAF5721",
                Set_TankNo(this.TXT01_JITANKNO.GetValue().ToString()).Trim()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                if (dt.Rows.Count == 1)
                {
                    this.CBH01_JICHJANG.SetValue(dt.Rows[0]["TNLOCATE"].ToString());
                }
            }
        }
        #endregion

        #region Description : 이전 HSN에 대한 데이터 가져오기
        private void UP_GET_PreIPMTQTY()
        {
            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_6ABKU341",
                this.DTP01_JIIPHANG.GetValue().ToString(),       // 입항일자
                this.CBH01_JIBONSUN.GetValue().ToString(),       // 본선
                this.CBH01_JIHWAJU.GetValue().ToString(),        // 화주
                this.CBH01_JIHWAMUL.GetValue().ToString(),       // 화물
                this.TXT01_JIBLNO.GetValue().ToString(),         // BL번호
                this.TXT01_JIMSNSEQ.GetValue().ToString(),       // MSN번호
                this.TXT01_JIHSNSEQ.GetValue().ToString()        // HSN번호
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.TXT01_JUNIPMTQTY.SetValue(dt.Rows[0]["IPMTQTY"].ToString());
            }
        }
        #endregion

        #region Description : 저장 ProcessCheck
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt = new DataTable();

            //if (fsGUBUN.ToString() == "NEW")
            //{
            //    // 출고지시 - 동일 지시 존재 체크
            //    this.DbConnector.CommandClear();
            //    this.DbConnector.Attach
            //        (
            //        "TY_P_UT_6AKB6431",
            //        Get_Date(this.DTP01_JIYYMM.GetValue().ToString())
            //        );

            //    dt = this.DbConnector.ExecuteDataTable();

            //    if (dt.Rows.Count > 0)
            //    {
            //        for (i = 0; i < dt.Rows.Count; i++)
            //        {
            //            if (Get_Date(this.DTP01_JIIPHANG.GetValue().ToString())   == dt.Rows[i]["JIIPHANG"].ToString() &&
            //                this.CBH01_JIBONSUN.GetValue().ToString().ToUpper()   == dt.Rows[i]["JIBONSUN"].ToString().ToUpper() &&
            //                this.CBH01_JIHWAJU.GetValue().ToString().ToUpper()    == dt.Rows[i]["JIHWAJU"].ToString().ToUpper() &&
            //                this.CBH01_JIHWAMUL.GetValue().ToString().ToUpper()   == dt.Rows[i]["JIHWAMUL"].ToString().ToUpper() &&
            //                this.TXT01_JIBLNO.GetValue().ToString().ToUpper()     == dt.Rows[i]["JIBLNO"].ToString().ToUpper() &&
            //                this.TXT01_JIMSNSEQ.GetValue().ToString()             == dt.Rows[i]["JIMSNSEQ"].ToString() &&
            //                this.TXT01_JIHSNSEQ.GetValue().ToString()             == dt.Rows[i]["JIHSNSEQ"].ToString() &&
            //                Get_Date(this.DTP01_JICUSTIL.GetValue().ToString())   == dt.Rows[i]["JICUSTIL"].ToString() &&
            //                this.TXT01_JICHASU.GetValue().ToString()              == dt.Rows[i]["JICHASU"].ToString() &&
            //                this.CBH01_JIACTHJ.GetValue().ToString().ToUpper()    == dt.Rows[i]["JIACTHJ"].ToString().ToUpper() &&
            //                this.TXT01_JIJGHWAJU.GetValue().ToString().ToUpper()  == dt.Rows[i]["JIJGHWAJU"].ToString().ToUpper() &&
            //                this.CBH01_JIYSHWAJU.GetValue().ToString().ToUpper()  == dt.Rows[i]["JIYSHWAJU"].ToString().ToUpper() &&
            //                this.CBH01_JIYDHWAJU.GetValue().ToString().ToUpper()  == dt.Rows[i]["JIYDHWAJU"].ToString().ToUpper() &&
            //                Get_Date(this.DTP01_JIYSDATE.GetValue().ToString())   == dt.Rows[i]["JIYSDATE"].ToString() &&
            //                this.TXT01_JIYDSEQ.GetValue().ToString().ToUpper()    == dt.Rows[i]["JIYDSEQ"].ToString() &&
            //                this.TXT01_JIYSSEQ.GetValue().ToString().ToUpper()    == dt.Rows[i]["JIYSSEQ"].ToString() &&
            //                Set_TankNo(this.TXT01_JITANKNO.GetValue().ToString()) == dt.Rows[i]["JITANKNO"].ToString() &&
            //                Set_TankNo(this.TXT01_JIIPTANK.GetValue().ToString()) == dt.Rows[i]["JIIPTANK"].ToString() &&
            //                this.CBH01_JICHHJ.GetValue().ToString().ToUpper()     == dt.Rows[i]["JICHHJ"].ToString().ToUpper() &&
            //                this.TXT01_JICARNO2.GetValue().ToString()             == dt.Rows[i]["JICARNO2"].ToString()
            //                )
            //            {
            //                this.ShowCustomMessage("TICK-NO :" + dt.Rows[i]["JISEQ"].ToString().ToUpper() + "번호에 이미 등록되어 있습니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            //                this.DTP01_JIYYMM.Focus();

            //                e.Successed = false;
            //                return;
            //            }
            //        }
            //    }
            //}

            //if (fsGUBUN.ToString() == "UPT")
            //{
            //    if (Get_Date(this.DTP01_JIIPHANG.GetValue().ToString())   != dt.Rows[i]["JIIPHANG"].ToString() &&
            //        this.CBH01_JIBONSUN.GetValue().ToString().ToUpper()   != dt.Rows[i]["JIBONSUN"].ToString().ToUpper() &&
            //        this.CBH01_JIHWAJU.GetValue().ToString().ToUpper()    != dt.Rows[i]["JIHWAJU"].ToString().ToUpper() &&
            //        this.CBH01_JIHWAMUL.GetValue().ToString().ToUpper()   != dt.Rows[i]["JIHWAMUL"].ToString().ToUpper() &&
            //        this.TXT01_JIBLNO.GetValue().ToString().ToUpper()     != dt.Rows[i]["JIBLNO"].ToString().ToUpper() &&
            //        this.TXT01_JIMSNSEQ.GetValue().ToString()             != dt.Rows[i]["JIMSNSEQ"].ToString() &&
            //        this.TXT01_JIHSNSEQ.GetValue().ToString()             != dt.Rows[i]["JIHSNSEQ"].ToString() &&
            //        Get_Date(this.DTP01_JICUSTIL.GetValue().ToString())   != dt.Rows[i]["JICUSTIL"].ToString() &&
            //        this.TXT01_JICHASU.GetValue().ToString()              != dt.Rows[i]["JICHASU"].ToString() &&
            //        this.CBH01_JIACTHJ.GetValue().ToString().ToUpper()    != dt.Rows[i]["JIACTHJ"].ToString().ToUpper() &&
            //        this.TXT01_JIJGHWAJU.GetValue().ToString().ToUpper()  != dt.Rows[i]["JIJGHWAJU"].ToString().ToUpper() &&
            //        this.CBH01_JIYSHWAJU.GetValue().ToString().ToUpper()  != dt.Rows[i]["JIYSHWAJU"].ToString().ToUpper() &&
            //        this.CBH01_JIYDHWAJU.GetValue().ToString().ToUpper()  != dt.Rows[i]["JIYDHWAJU"].ToString().ToUpper() &&
            //        Get_Date(this.DTP01_JIYSDATE.GetValue().ToString())   != dt.Rows[i]["JIYSDATE"].ToString() &&
            //        this.TXT01_JIYDSEQ.GetValue().ToString().ToUpper()    != dt.Rows[i]["JIYDSEQ"].ToString() &&
            //        this.TXT01_JIYSSEQ.GetValue().ToString().ToUpper()    != dt.Rows[i]["JIYSSEQ"].ToString()
            //        )
            //    {
            //        this.ShowMessage("TY_M_UT_6AKBD432");
            //        this.DTP01_JIYYMM.Focus();

            //        e.Successed = false;
            //        return;
            //    }

            //    // 출고지시 - 동일 지시 존재 체크
            //    this.DbConnector.CommandClear();
            //    this.DbConnector.Attach
            //        (
            //        "TY_P_UT_6AKB6431",
            //        Get_Date(this.DTP01_JIYYMM.GetValue().ToString())
            //        );

            //    dt = this.DbConnector.ExecuteDataTable();

            //    if (dt.Rows.Count > 0)
            //    {
            //        for (i = 0; i < dt.Rows.Count; i++)
            //        {
            //            if (Get_Date(this.DTP01_JIIPHANG.GetValue().ToString())   == dt.Rows[i]["JIIPHANG"].ToString() &&
            //                this.CBH01_JIBONSUN.GetValue().ToString().ToUpper()   == dt.Rows[i]["JIBONSUN"].ToString().ToUpper() &&
            //                this.CBH01_JIHWAJU.GetValue().ToString().ToUpper()    == dt.Rows[i]["JIHWAJU"].ToString().ToUpper() &&
            //                this.CBH01_JIHWAMUL.GetValue().ToString().ToUpper()   == dt.Rows[i]["JIHWAMUL"].ToString().ToUpper() &&
            //                this.TXT01_JIBLNO.GetValue().ToString().ToUpper()     == dt.Rows[i]["JIBLNO"].ToString().ToUpper() &&
            //                this.TXT01_JIMSNSEQ.GetValue().ToString()             == dt.Rows[i]["JIMSNSEQ"].ToString() &&
            //                this.TXT01_JIHSNSEQ.GetValue().ToString()             == dt.Rows[i]["JIHSNSEQ"].ToString() &&
            //                Get_Date(this.DTP01_JICUSTIL.GetValue().ToString())   == dt.Rows[i]["JICUSTIL"].ToString() &&
            //                this.TXT01_JICHASU.GetValue().ToString()              == dt.Rows[i]["JICHASU"].ToString() &&
            //                this.CBH01_JIACTHJ.GetValue().ToString().ToUpper()    == dt.Rows[i]["JIACTHJ"].ToString().ToUpper() &&
            //                this.TXT01_JIJGHWAJU.GetValue().ToString().ToUpper()  == dt.Rows[i]["JIJGHWAJU"].ToString().ToUpper() &&
            //                this.CBH01_JIYSHWAJU.GetValue().ToString().ToUpper()  == dt.Rows[i]["JIYSHWAJU"].ToString().ToUpper() &&
            //                this.CBH01_JIYDHWAJU.GetValue().ToString().ToUpper()  == dt.Rows[i]["JIYDHWAJU"].ToString().ToUpper() &&
            //                Get_Date(this.DTP01_JIYSDATE.GetValue().ToString())   == dt.Rows[i]["JIYSDATE"].ToString() &&
            //                this.TXT01_JIYDSEQ.GetValue().ToString().ToUpper()    == dt.Rows[i]["JIYDSEQ"].ToString() &&
            //                this.TXT01_JIYSSEQ.GetValue().ToString().ToUpper()    == dt.Rows[i]["JIYSSEQ"].ToString() &&
            //                Set_TankNo(this.TXT01_JITANKNO.GetValue().ToString()) == dt.Rows[i]["JITANKNO"].ToString() &&
            //                Set_TankNo(this.TXT01_JIIPTANK.GetValue().ToString()) == dt.Rows[i]["JIIPTANK"].ToString() &&
            //                this.CBH01_JICHHJ.GetValue().ToString().ToUpper()     == dt.Rows[i]["JICHHJ"].ToString().ToUpper() &&
            //                this.TXT01_JICARNO2.GetValue().ToString()             == dt.Rows[i]["JICARNO2"].ToString()
            //                )
            //            {
            //                this.ShowCustomMessage("TICK-NO :" + dt.Rows[i]["JISEQ"].ToString().ToUpper() + "번호에 이미 등록되어 있습니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            //                this.DTP01_JIYYMM.Focus();

            //                e.Successed = false;
            //                return;
            //            }
            //        }
            //    }
            //}


            // TANK LORRY이면 씰번호 입력 안함
            if (this.CBO01_JIWKTYPE.GetValue().ToString() == "01")
            {
                this.CBO01_JISILNUMCK.SetValue("");
            }


            if (fsGUBUN.ToString() == "UPT")
            {
                if (this.TXT01_ORDATE.GetValue().ToString() != "" && this.TXT01_ORSEQ.GetValue().ToString() != "")
                {
                    if ((fsJIJGHWAJU.ToString() != this.TXT01_JIJGHWAJU.GetValue().ToString()) || (fsJIHWAMUL.ToString() != this.CBH01_JIHWAMUL.GetValue().ToString()))
                    {
                        this.ShowMessage("TY_M_UT_842I8800");
                        this.CBO01_JIJICNT.Focus();

                        e.Successed = false;
                        return;
                    }
                }
            }

            if (int.Parse(Get_Numeric(this.CBO01_JIJICNT.GetValue().ToString())) == 0)
            {
                this.ShowMessage("TY_M_UT_7B1DX924");
                this.CBO01_JIJICNT.Focus();

                e.Successed = false;
                return;
            }

            // 재고 화주 존재 유무 체크
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_66FAH184",
                "HJ",
                this.TXT01_JIJGHWAJU.GetValue().ToString().ToUpper(),
                ""
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count <= 0)
            {
                this.ShowMessage("TY_M_UT_6C9GW055");
                this.TXT01_JIJGHWAJU.Focus();

                e.Successed = false;
                return;
            }

            // BL별 입고값 가져오기
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_6AKBK434",
                Get_Date(this.DTP01_JIIPHANG.GetValue().ToString()),
                this.CBH01_JIBONSUN.GetValue().ToString().ToUpper(),
                this.CBH01_JIHWAJU.GetValue().ToString().ToUpper(),
                this.CBH01_JIHWAMUL.GetValue().ToString().ToUpper(),
                this.TXT01_JIBLNO.GetValue().ToString().ToUpper(),
                this.TXT01_JIMSNSEQ.GetValue().ToString(),
                this.TXT01_JIHSNSEQ.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                // 입고수량
                this.TXT01_JIIPQTY.SetValue(dt.Rows[0]["IPMTQTY"].ToString());
                // 통관누계
                this.TXT01_IPPAQTY.SetValue(dt.Rows[0]["IPPAQTY"].ToString());
                // 출고누계
                this.TXT01_IPCHQTY.SetValue(dt.Rows[0]["IPCHQTY"].ToString());
                // 통관재고
                this.TXT01_JIJEQTY.SetValue(dt.Rows[0]["IPJEQTY"].ToString());
                // 미통관재고
                this.TXT01_JIJANQTY.SetValue(dt.Rows[0]["IPJANQTY"].ToString());
            }

            // 이전 HSN에 대한 데이터 가져오기
            UP_GET_PreIPMTQTY();

            // 미통관잔량 = MT입고량 - 통관량 - BL분할 수량	
            this.TXT01_JIJANQTY.SetValue(String.Format("{0,9:N3}", double.Parse(this.TXT01_JIIPQTY.GetValue().ToString()) - double.Parse(this.TXT01_IPPAQTY.GetValue().ToString()) - double.Parse(this.TXT01_JUNIPMTQTY.GetValue().ToString())));

            // 출고탱크 체크
            if (this.TXT01_JITANKNO.GetValue().ToString() != "")
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_UT_6AKBS435",
                    Set_TankNo(this.TXT01_JITANKNO.GetValue().ToString()).Trim()
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count <= 0)
                {
                    this.ShowMessage("TY_M_UT_6AKBT436");
                    this.TXT01_JITANKNO.Focus();

                    e.Successed = false;
                    return;
                }
            }

            // 입고탱크 체크
            if (this.TXT01_JIIPTANK.GetValue().ToString() != "")
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_UT_6AKBS435",
                    this.TXT01_JIIPTANK.GetValue().ToString()
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count <= 0)
                {
                    this.ShowMessage("TY_M_UT_6AKBV437");
                    this.TXT01_JIIPTANK.Focus();

                    e.Successed = false;
                    return;
                }
            }



            // 입고화물 체크
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_6AKBY438",
                Get_Date(this.DTP01_JIIPHANG.GetValue().ToString()),
                this.CBH01_JIBONSUN.GetValue().ToString().ToUpper(),
                this.CBH01_JIHWAJU.GetValue().ToString().ToUpper(),
                this.CBH01_JIHWAMUL.GetValue().ToString().ToUpper()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count <= 0)
            {
                this.ShowMessage("TY_M_UT_6AKBY439");
                SetFocus(this.TXT01_JIJGHWAJU);

                e.Successed = false;
                return;
            }


            // B/L별 입고 체크
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_6AKBZ440",
                Get_Date(this.DTP01_JIIPHANG.GetValue().ToString()),
                this.CBH01_JIBONSUN.GetValue().ToString().ToUpper(),
                this.CBH01_JIHWAJU.GetValue().ToString().ToUpper(),
                this.CBH01_JIHWAMUL.GetValue().ToString().ToUpper(),
                this.TXT01_JIBLNO.GetValue().ToString().ToUpper(),
                this.TXT01_JIMSNSEQ.GetValue().ToString(),
                this.TXT01_JIHSNSEQ.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count <= 0)
            {
                this.ShowMessage("TY_M_UT_6AKBZ441");
                SetFocus(this.TXT01_JIJGHWAJU);

                e.Successed = false;
                return;
            }

            // 이고가 없을 경우 출고탱크와 입고탱크 번호가 일치해야 함
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_752FQ401",
                Get_Date(this.DTP01_JIIPHANG.GetValue().ToString()),
                this.CBH01_JIBONSUN.GetValue().ToString().ToUpper(),
                this.CBH01_JIHWAJU.GetValue().ToString().ToUpper(),
                this.CBH01_JIHWAMUL.GetValue().ToString().ToUpper(),
                this.TXT01_JITANKNO.GetValue().ToString().Trim()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                if (this.TXT01_JITANKNO.GetValue().ToString().Trim() != this.TXT01_JIIPTANK.GetValue().ToString().Trim())
                {
                    this.ShowMessage("TY_M_UT_752FW402");
                    SetFocus(this.TXT01_JIJGHWAJU);

                    e.Successed = false;
                    return;
                }
            }


            // 여기부터 할 차례

            // SURVEY파일 체크
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_6AKCN442",
                Get_Date(this.DTP01_JIIPHANG.GetValue().ToString()),
                this.CBH01_JIBONSUN.GetValue().ToString().ToUpper(),
                this.CBH01_JIHWAJU.GetValue().ToString().ToUpper(),
                this.CBH01_JIHWAMUL.GetValue().ToString().ToUpper(),
                this.TXT01_JITANKNO.GetValue().ToString().Trim()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count <= 0)
            {
                this.ShowMessage("TY_M_UT_6AKCN443");
                SetFocus(this.TXT01_JIJGHWAJU);

                e.Successed = false;
                return;
            }

            // 매출입고 할증 체크
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_6AKCN444",
                Get_Date(this.DTP01_JIIPHANG.GetValue().ToString()),
                this.CBH01_JIBONSUN.GetValue().ToString().ToUpper(),
                this.CBH01_JIHWAJU.GetValue().ToString().ToUpper(),
                this.CBH01_JIHWAMUL.GetValue().ToString().ToUpper(),
                this.TXT01_JIIPTANK.GetValue().ToString().Trim()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count <= 0)
            {
                this.ShowMessage("TY_M_UT_6AKCO445");
                SetFocus(this.TXT01_JIJGHWAJU);

                e.Successed = false;
                return;
            }


            // 통관화주 체크
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_6AKDD446",
                this.CBH01_JIACTHJ.GetValue().ToString().ToUpper(),
                Get_Date(this.DTP01_JIIPHANG.GetValue().ToString()),
                this.CBH01_JIBONSUN.GetValue().ToString().ToUpper(),
                this.CBH01_JIHWAJU.GetValue().ToString().ToUpper(),
                this.CBH01_JIHWAMUL.GetValue().ToString().ToUpper(),
                this.TXT01_JIBLNO.GetValue().ToString().ToUpper(),
                this.TXT01_JIMSNSEQ.GetValue().ToString(),
                this.TXT01_JIHSNSEQ.GetValue().ToString(),
                Get_Date(this.DTP01_JICUSTIL.GetValue().ToString()),
                this.TXT01_JICHASU.GetValue().ToString(),
                this.TXT01_JIJGHWAJU.GetValue().ToString().ToUpper(),
                this.CBH01_JIYSHWAJU.GetValue().ToString().ToUpper(),
                this.CBH01_JIYDHWAJU.GetValue().ToString().ToUpper(),
                Get_Date(this.DTP01_JIYSDATE.GetValue().ToString()),
                this.TXT01_JIYDSEQ.GetValue().ToString().ToUpper(),
                this.TXT01_JIYSSEQ.GetValue().ToString().ToUpper()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count <= 0)
            {
                this.ShowMessage("TY_M_UT_6AKDD447");
                SetFocus(this.TXT01_JIJGHWAJU);

                e.Successed = false;
                return;
            }
            else
            {
                fsJIJGHWAJU = dt.Rows[0]["CJJGHWAJU"].ToString();
            }

            // 양수도일 경우 체크
            if (this.CBO01_JIYNGUBUN.GetValue().ToString() == "R")
            {
                // 양수도 체크
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_UT_699AD123",
                    Get_Date(this.DTP01_JIIPHANG.GetValue().ToString()),
                    this.CBH01_JIBONSUN.GetValue().ToString().ToUpper(),
                    this.CBH01_JIHWAJU.GetValue().ToString().ToUpper(),
                    this.CBH01_JIHWAMUL.GetValue().ToString().ToUpper(),
                    this.TXT01_JIBLNO.GetValue().ToString().ToUpper(),
                    this.TXT01_JIMSNSEQ.GetValue().ToString(),
                    this.TXT01_JIHSNSEQ.GetValue().ToString(),
                    Get_Date(this.DTP01_JICUSTIL.GetValue().ToString()),
                    this.TXT01_JICHASU.GetValue().ToString(),
                    this.CBH01_JIACTHJ.GetValue().ToString().ToUpper(),
                    this.CBH01_JIYDHWAJU.GetValue().ToString().ToUpper(),
                    this.CBH01_JIYSHWAJU.GetValue().ToString().ToUpper(),
                    Get_Date(this.DTP01_JIYSDATE.GetValue().ToString()),
                    this.TXT01_JIYDSEQ.GetValue().ToString().ToUpper(),
                    this.TXT01_JIYSSEQ.GetValue().ToString().ToUpper()
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count <= 0)
                {
                    this.ShowMessage("TY_M_UT_6C9H8056");
                    SetFocus(this.TXT01_JIJGHWAJU);

                    e.Successed = false;
                    return;
                }
            }


            // 통관화일 체크
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_6BAGW725",
                Get_Date(this.DTP01_JIIPHANG.GetValue().ToString()),
                this.CBH01_JIBONSUN.GetValue().ToString().ToUpper(),
                this.CBH01_JIHWAJU.GetValue().ToString().ToUpper(),
                this.CBH01_JIHWAMUL.GetValue().ToString().ToUpper(),
                this.TXT01_JIBLNO.GetValue().ToString().ToUpper(),
                this.TXT01_JIMSNSEQ.GetValue().ToString(),
                this.TXT01_JIHSNSEQ.GetValue().ToString(),
                Get_Date(this.DTP01_JICUSTIL.GetValue().ToString()),
                this.TXT01_JICHASU.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count <= 0)
            {
                this.ShowMessage("TY_M_UT_6BAGY726");
                SetFocus(this.TXT01_JIJGHWAJU);

                e.Successed = false;
                return;
            }
            else
            {
                if (dt.Rows[0]["CSBANGB"].ToString() == "70" || dt.Rows[0]["CSBANGB"].ToString() == "71")
                {
                    this.ShowMessage("TY_M_UT_6BAGY727");
                    SetFocus(this.TXT01_JIJGHWAJU);

                    e.Successed = false;
                    return;
                }
            }

            if (this.TXT01_JIJGHWAJU.GetValue().ToString() != fsJIJGHWAJU.ToString())
            {
                this.ShowMessage("TY_M_UT_6BAH0728");
                SetFocus(this.TXT01_JIJGHWAJU);

                e.Successed = false;
                return;
            }

            if (this.TXT01_JICARNO2.GetValue().ToString() == "" && this.TXT01_JICONTNUM.GetValue().ToString() == "")
            {
                this.ShowMessage("TY_M_UT_6BAH8731");
                SetFocus(this.TXT01_JICARNO2);

                e.Successed = false;
                return;
            }

            if (this.TXT01_JICARNO2.GetValue().ToString() != "")
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_UT_6BAH0732",
                    this.TXT01_JICARNO2.GetValue().ToString()
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count <= 0)
                {
                    this.ShowMessage("TY_M_UT_6BAH2733");
                    SetFocus(this.TXT01_JICARNO2);

                    e.Successed = false;
                    return;
                }
            }

            if ((double.Parse(Get_Numeric(this.TXT01_JISTMTQTY.GetValue().ToString())) == 0 && double.Parse(Get_Numeric(this.TXT01_JIEDMTQTY.GetValue().ToString())) == 0) &&
                (double.Parse(Get_Numeric(this.TXT01_JISTLTQTY.GetValue().ToString())) == 0 && double.Parse(Get_Numeric(this.TXT01_JIEDLTQTY.GetValue().ToString())) == 0))
            {
                if (double.Parse(Get_Numeric(this.TXT01_JISTLTQTY.GetValue().ToString())) == 0 && double.Parse(Get_Numeric(this.TXT01_JIEDLTQTY.GetValue().ToString())) == 0)
                {
                    this.ShowMessage("TY_M_UT_6BAH6730");
                    SetFocus(this.TXT01_JISTMTQTY);

                    e.Successed = false;
                    return;
                }
            }

            //if (this.TXT01_JIJGHWAJU.GetValue().ToString() == "GSG")
            //{
            //    if (double.Parse(Get_Numeric(this.TXT01_JISTLTQTY.GetValue().ToString())) == 0 && double.Parse(Get_Numeric(this.TXT01_JIEDLTQTY.GetValue().ToString())) == 0)
            //    {
            //        this.ShowMessage("TY_M_UT_6BAH4729");
            //        SetFocus(this.TXT01_JISTLTQTY);

            //        e.Successed = false;
            //        return;
            //    }
            //}

            if (this.CBO01_JIWKTYPE.GetValue().ToString() == "02" || this.CBO01_JIWKTYPE.GetValue().ToString() == "03")
            {
                if (this.TXT01_JICONTNUM.GetValue().ToString() == "")
                {
                    this.ShowMessage("TY_M_UT_7AHGM833");
                    SetFocus(this.TXT01_JICONTNUM);

                    e.Successed = false;
                    return;
                }
            }
            else
            {
                if (this.TXT01_JICARNO2.GetValue().ToString() == "")
                {
                    this.ShowMessage("TY_M_UT_7AHGM834");
                    SetFocus(this.TXT01_JICARNO2);

                    e.Successed = false;
                    return;
                }
            }

            if (this.CBO01_JIWKTYPE.GetValue().ToString() == "02" || this.CBO01_JIWKTYPE.GetValue().ToString() == "03")
            {
                if (this.TXT01_JICONTNUM.GetValue().ToString().Length != 11)
                {
                    this.ShowMessage("TY_M_UT_7AHHI835");
                    SetFocus(this.TXT01_JICONTNUM);

                    e.Successed = false;
                    return;
                }
            }

            if (this.CBO01_JIWKTYPE.GetValue().ToString() == "03")
            {
                if (this.CBH01_JIDNST.GetValue().ToString() == "" && this.TXT01_JIJGHWAJU.GetValue().ToString() == "GSG")
                {
                    this.ShowMessage("TY_M_UT_81JB1490");
                    SetFocus(this.CBH01_JIDNST.CodeText);

                    e.Successed = false;
                    return;
                }
            }

            if (this.CBO01_JIWKTYPE.GetValue().ToString() == "02" || this.CBO01_JIWKTYPE.GetValue().ToString() == "03")
            {
                if (this.TXT01_JICONTNUM.GetValue().ToString() != "")
                {
                    string sCONTNUM = string.Empty;

                    sCONTNUM = this.TXT01_JICONTNUM.GetValue().ToString();

                    for (int i = 0; i < 11; i++)
                    {
                        if (i == 0 || i == 1 || i == 2 || i == 3)
                        {
                            if (sCONTNUM.Substring(i, 1).ToString() != "A" &&
                                sCONTNUM.Substring(i, 1).ToString() != "B" &&
                                sCONTNUM.Substring(i, 1).ToString() != "C" &&
                                sCONTNUM.Substring(i, 1).ToString() != "D" &&
                                sCONTNUM.Substring(i, 1).ToString() != "E" &&
                                sCONTNUM.Substring(i, 1).ToString() != "F" &&
                                sCONTNUM.Substring(i, 1).ToString() != "G" &&
                                sCONTNUM.Substring(i, 1).ToString() != "H" &&
                                sCONTNUM.Substring(i, 1).ToString() != "I" &&
                                sCONTNUM.Substring(i, 1).ToString() != "J" &&
                                sCONTNUM.Substring(i, 1).ToString() != "K" &&
                                sCONTNUM.Substring(i, 1).ToString() != "L" &&
                                sCONTNUM.Substring(i, 1).ToString() != "M" &&
                                sCONTNUM.Substring(i, 1).ToString() != "N" &&
                                sCONTNUM.Substring(i, 1).ToString() != "O" &&
                                sCONTNUM.Substring(i, 1).ToString() != "P" &&
                                sCONTNUM.Substring(i, 1).ToString() != "Q" &&
                                sCONTNUM.Substring(i, 1).ToString() != "R" &&
                                sCONTNUM.Substring(i, 1).ToString() != "S" &&
                                sCONTNUM.Substring(i, 1).ToString() != "T" &&
                                sCONTNUM.Substring(i, 1).ToString() != "U" &&
                                sCONTNUM.Substring(i, 1).ToString() != "V" &&
                                sCONTNUM.Substring(i, 1).ToString() != "W" &&
                                sCONTNUM.Substring(i, 1).ToString() != "X" &&
                                sCONTNUM.Substring(i, 1).ToString() != "Y" &&
                                sCONTNUM.Substring(i, 1).ToString() != "Z")
                            {
                                this.ShowMessage("TY_M_UT_7AHGJ830");
                                SetFocus(this.TXT01_JICONTNUM);

                                e.Successed = false;
                                return;
                            }
                        }
                        else
                        {

                            if (sCONTNUM.Substring(i, 1).ToString() != "0" &&
                                sCONTNUM.Substring(i, 1).ToString() != "1" &&
                                sCONTNUM.Substring(i, 1).ToString() != "2" &&
                                sCONTNUM.Substring(i, 1).ToString() != "3" &&
                                sCONTNUM.Substring(i, 1).ToString() != "4" &&
                                sCONTNUM.Substring(i, 1).ToString() != "5" &&
                                sCONTNUM.Substring(i, 1).ToString() != "6" &&
                                sCONTNUM.Substring(i, 1).ToString() != "7" &&
                                sCONTNUM.Substring(i, 1).ToString() != "8" &&
                                sCONTNUM.Substring(i, 1).ToString() != "9")
                            {
                                this.ShowMessage("TY_M_UT_7AHGJ832");
                                SetFocus(this.TXT01_JICONTNUM);

                                e.Successed = false;
                                return;
                            }
                        }
                    }
                }
            }


            string sCHJISINUM = string.Empty;

            sCHJISINUM = Get_Date(this.DTP01_JIYYMM.GetValue().ToString()) + Set_Fill3(this.TXT01_JISEQ.GetValue().ToString());

            // 출고가 되었으면 수정 불가
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_6BB94734",
                sCHJISINUM.ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.ShowMessage("TY_M_UT_6AKBJ433");
                this.CBH01_JIYSHWAJU.Focus();

                e.Successed = false;
                return;
            }

            // 저장하시겠습니까?
            if (!this.ShowMessage("TY_M_GB_23NAD871"))
            {
                e.Successed = false;
                return;
            }
        }
        #endregion

        #region Description : 삭제 ProcessCheck
        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            string sCHJISINUM = string.Empty;

            sCHJISINUM = Get_Date(this.DTP01_JIYYMM.GetValue().ToString()) + Set_Fill3(this.TXT01_JISEQ.GetValue().ToString());

            DataTable dt = new DataTable();

            // 출고가 되었으면 삭제 불가
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_6BB94734",
                sCHJISINUM.ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.ShowMessage("TY_M_UT_689E4006");
                this.CBH01_JIYSHWAJU.Focus();

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

        #region Description : 필드 클리어
        private void UP_FieldClear()
        {
            this.TXT01_JISEQ.SetValue("");

            //this.DTP01_JIIPHANG.SetValue("");    // 입항일자
            //this.CBH01_JIBONSUN.SetValue("");    // 본선
            //this.CBH01_JIHWAJU.SetValue("");     // 화주
            //this.CBH01_JIHWAMUL.SetValue("");    // 화물
            //this.TXT01_JIBLNO.SetValue("");      // BL번호
            //this.TXT01_JIMSNSEQ.SetValue("");    // MSN번호
            //this.TXT01_JIHSNSEQ.SetValue("");    // HSN번호
            //this.DTP01_JICUSTIL.SetValue("");    // 통관일자
            //this.TXT01_JICHASU.SetValue("");     // 통관차수
            //this.TXT01_JIJGHWAJU.SetValue("");   // 재고화주
            //this.TXT01_JIJGHWAJUNM.SetValue(""); // 재고화주명
            //this.CBH01_JIACTHJ.SetValue("");     // 통관화주
            //this.CBH01_JIYDHWAJU.SetValue("");   // 양도화주
            //this.CBH01_JIYSHWAJU.SetValue("");   // 양수화주
            //this.DTP01_JIYSDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));  // 양수일자
            //this.TXT01_JIYSSEQ.SetValue("");     // 양수순번
            //this.TXT01_JIYDSEQ.SetValue("");     // 양도차수



            //this.CBH01_JICHHJ.SetValue("");      // 출고화주
            //this.TXT01_JIIPQTY.SetValue("");
            //this.TXT01_JUNIPMTQTY.SetValue("");
            //this.TXT01_IPPAQTY.SetValue("");
            //this.TXT01_IPCHQTY.SetValue("");
            //this.TXT01_JIJEQTY.SetValue("");
            //this.TXT01_JIJANQTY.SetValue("");
            //this.TXT01_CJCUQTY.SetValue("");
            //this.TXT01_CJCHQTY.SetValue("");
            //this.TXT01_CJJEQTY.SetValue("");
            //this.TXT01_SUMCHQTY.SetValue("");
            //this.TXT01_JITANKNO.SetValue("");
            //this.TXT01_JIIPTANK.SetValue("");
            //this.CBH01_JIHISAB.SetValue("");
            //this.CBH01_JICHJANG.SetValue("");
            //this.TXT01_JICARNO1.SetValue("");
            //this.TXT01_JICARNO2.SetValue("");
            //this.TXT01_JISTMTQTY.SetValue("");

            this.CBO01_JIJICNT.SetValue("1");
        }
        #endregion

        #region Description : 텍스트 ReadOnly
        private void UP_Set_ReadOnly(string sGUBUN)
        {
            if (sGUBUN == "NEW")
            {
                this.DTP01_JIYYMM.SetReadOnly(false);
                this.TXT01_JISEQ.SetReadOnly(false);
            }
            else
            {
                this.DTP01_JIYYMM.SetReadOnly(true);
                this.TXT01_JISEQ.SetReadOnly(true);
            }
        }
        #endregion

        #region Description : 버튼 Visible
        private void UP_BUTTON_Visible(string sGUBUN)
        {
            if (sGUBUN.ToString() == "NEW")
            {
                this.BTN61_SAV.Visible = true;
                this.BTN61_REM.Visible = false;
            }
            else if (sGUBUN.ToString() == "UPT")
            {
                this.BTN61_SAV.Visible = true;
                this.BTN61_REM.Visible = true;
            }
            else if (sGUBUN.ToString() == "")
            {
                this.BTN61_SAV.Visible = false;
                this.BTN61_REM.Visible = false;
            }
        }
        #endregion

        #region Description : 스프레드 이벤트
        private void FPS91_TY_S_UT_6AJJ1427_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            this.DTP01_JIYYMM.SetValue(this.FPS91_TY_S_UT_6AJJ1427.GetValue("JIYYMM").ToString());
            this.TXT01_JISEQ.SetValue(this.FPS91_TY_S_UT_6AJJ1427.GetValue("JISEQ").ToString());

            // 확인
            UP_RUN();
        }
        #endregion

        #region Description : 양수량 텍스트박스 이벤트
        private void TXT01_YNQTY_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (this.BTN61_SAV.Visible == true)
                {
                    SetFocus(this.BTN61_SAV);
                }
            }
        }
        #endregion

        #region Description : 재고화주 이벤트
        private void BTN61_JGHWAJU_Click(object sender, EventArgs e)
        {
            UP_CALL_JEGO();

            if (Get_Date(this.DTP01_JIIPHANG.GetValue().ToString()) != "" && this.CBH01_JIBONSUN.GetValue().ToString() != "" &&
                this.CBH01_JIHWAJU.GetValue().ToString() != "" && this.CBH01_JIHWAMUL.GetValue().ToString() != "")
            {
                //// 출고탱크
                //UP_GET_JITANKNO();

                // 입고탱크
                UP_GET_JIIPTANK();

                SetFocus(this.CBH01_JICHHJ.CodeText);
            }
        }

        private void TXT01_JIJGHWAJU_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.F1)
            {
                UP_CALL_JEGO();

                if (Get_Date(this.DTP01_JIIPHANG.GetValue().ToString()) != "" && this.CBH01_JIBONSUN.GetValue().ToString() != "" &&
                    this.CBH01_JIHWAJU.GetValue().ToString() != "" && this.CBH01_JIHWAMUL.GetValue().ToString() != "")
                {
                    // 출고탱크
                    //UP_GET_JITANKNO();

                    // 입고탱크
                    UP_GET_JIIPTANK();

                    SetFocus(this.CBH01_JICHHJ.CodeText);
                }
            }
        }
        #endregion

        #region Description : 출고탱크 이벤트
        private void BTN61_CHTANK_Click(object sender, EventArgs e)
        {
            TYUTGB009S popup = new TYUTGB009S(this.DTP01_JIIPHANG.GetValue().ToString(), this.CBH01_JIBONSUN.GetValue().ToString(),
                                              this.CBH01_JIHWAJU.GetValue().ToString(), this.CBH01_JIHWAMUL.GetValue().ToString());

            if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.TXT01_JITANKNO.SetValue(popup.fsTANKNO); // 출고탱크

                // 출하장 가져오기
                UP_GET_JICHJANG();

                SetFocus(this.TXT01_JIIPTANK);
            }
        }

        private void TXT01_JITANKNO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.F1)
            {
                TYUTGB009S popup = new TYUTGB009S(this.DTP01_JIIPHANG.GetValue().ToString(), this.CBH01_JIBONSUN.GetValue().ToString(),
                                              this.CBH01_JIHWAJU.GetValue().ToString(), this.CBH01_JIHWAMUL.GetValue().ToString());

                if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    this.TXT01_JITANKNO.SetValue(popup.fsTANKNO); // 출고탱크
                    SetFocus(this.TXT01_JIIPTANK);
                }
            }
        }
        #endregion

        #region Description : 입고탱크 이벤트
        private void BTN61_IPTANK_Click(object sender, EventArgs e)
        {
            TYUTGB010S popup = new TYUTGB010S(this.DTP01_JIIPHANG.GetValue().ToString(), this.CBH01_JIBONSUN.GetValue().ToString(),
                                              this.CBH01_JIHWAJU.GetValue().ToString(), this.CBH01_JIHWAMUL.GetValue().ToString());

            if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.TXT01_JIIPTANK.SetValue(popup.fsTANKNO); // 출고탱크
                SetFocus(this.CBH01_JICHJANG.CodeText);
            }
        }

        private void TXT01_JIIPTANK_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.F1)
            {
                TYUTGB010S popup = new TYUTGB010S(this.DTP01_JIIPHANG.GetValue().ToString(), this.CBH01_JIBONSUN.GetValue().ToString(),
                                              this.CBH01_JIHWAJU.GetValue().ToString(), this.CBH01_JIHWAMUL.GetValue().ToString());

                if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    this.TXT01_JIIPTANK.SetValue(popup.fsTANKNO); // 출고탱크
                    SetFocus(this.CBH01_JICHJANG.CodeText);
                }
            }
        }
        #endregion

        #region Description : 차량번호 이벤트
        private void BTN61_CARNO_Click(object sender, EventArgs e)
        {
            TYUTGB011S popup = new TYUTGB011S(this.TXT01_JICARNO2.GetValue().ToString());

            if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.TXT01_JICARNO2.SetValue(popup.fsCARNUMBER); // 차량번호
                SetFocus(this.TXT01_JISTMTQTY);
            }
        }

        private void TXT01_JICARNO2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.F1)
            {
                TYUTGB011S popup = new TYUTGB011S(this.TXT01_JICARNO2.GetValue().ToString());

                if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    this.TXT01_JICARNO2.SetValue(popup.fsCARNUMBER); // 차량번호
                    SetFocus(this.TXT01_JISTMTQTY);
                }
            }
        }
        #endregion

        #region Description : 통관화주별 출고조회 버튼 이벤트
        private void BTN65_UTTCODEHELP6_Click(object sender, EventArgs e)
        {
            TYUTGB012S popup = new TYUTGB012S("");

            if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
            }
        }
        #endregion

        #region Description : 지시 일괄 등록
        private void BTN61_UTTCODEHELP1_Click(object sender, EventArgs e)
        {
            if ((new TYUTGB014S()).ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.BTN61_INQ_Click(null, null);
            }
        }
        #endregion

        #region Description : 조회 종료일자 이벤트
        private void DTP01_EDIPHANG_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Enter)
            {
                this.BTN61_INQ_Click(null, null);
            }
        }
        #endregion

        #region Description : 차량관리
        private void BTN61_UTTCODEHELP2_Click(object sender, EventArgs e)
        {
            if ((new TYUTAU006I()).ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
            }
        }
        #endregion

        #region Description : 오더화주 이벤트
        private void CBH01_SHWAJU_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                SetFocus(this.BTN62_INQ);
            }
        }
        #endregion

        #region Description : 차량대수
        private void CBO01_JIJICNT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (this.BTN61_SAV.Visible == true)
                {
                    SetFocus(this.BTN61_SAV);
                }
            }
        }
        #endregion

        #region Description : 적용 기본 셋팅
        private void UP_Get_MemoLogList()
        {

            DateTime dTime = Convert.ToDateTime(Set_Date(this.DTP01_JIYYMM.GetString().ToString())).AddMonths(-2);

            string sSdate = dTime.Year.ToString() + Set_Fill2(dTime.Month.ToString()) + Set_Fill2(dTime.Day.ToString());

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_777H2050", sSdate, this.DTP01_JIYYMM.GetString().ToString(), TYUserInfo.EmpNo);
            DataTable dt = this.DbConnector.ExecuteDataTable();
            if (dt.Rows.Count > 0)
            {
                AutoCompleteStringCollection aclist = null;
                aclist = new AutoCompleteStringCollection();

                foreach (DataRow dr in dt.Rows)
                {
                    aclist.Add(dr[0].ToString());
                }
                this.TXT01_JICARNO1.AutoCompleteCustomSource = aclist;
            }
        }
        #endregion

        #region Description : 클리어 버튼
        private void BTN61_UTTCLEAR_Click(object sender, EventArgs e)
        {
            string sSTDATE = string.Empty;
            string sEDDATE = string.Empty;
            string sJISEQ1 = string.Empty;
            string sCHCARNO = string.Empty;
            string sJIYYMM = string.Empty;
            string sJISEQ = string.Empty;


            sSTDATE = this.DTP01_STIPHANG.GetValue().ToString();
            sEDDATE = this.DTP01_EDIPHANG.GetValue().ToString();
            sJISEQ1 = this.TXT01_JISEQ1.GetValue().ToString();
            sCHCARNO = this.TXT01_CHCARNO.GetValue().ToString();

            sJIYYMM = this.DTP01_JIYYMM.GetValue().ToString();
            sJISEQ = this.TXT01_JISEQ.GetValue().ToString();

            this.Initialize_Controls("01");

            this.DTP01_STIPHANG.SetValue(sSTDATE.ToString());
            this.DTP01_EDIPHANG.SetValue(sEDDATE.ToString());
            this.TXT01_JISEQ1.SetValue(sJISEQ1.ToString());
            this.TXT01_CHCARNO.SetValue(sCHCARNO.ToString());

            this.DTP01_JIYYMM.SetValue(sJIYYMM.ToString());
            this.TXT01_JISEQ.SetValue(sJISEQ.ToString());

            this.CBO01_JICHTYPE.SetValue("1");
            this.CBO01_JIUNIT.SetValue("1");
            this.CBO01_JIWKTYPE.SetValue("01");
            this.CBO01_JITMGUBN.SetValue("1");
            this.CBO01_JIJICNT.SetValue("1");

            UP_SEARCH();
        }
        #endregion

        #region Description : 입고지시 일괄등록
        private void BTN61_UTTCODEHELP5_Click(object sender, EventArgs e)
        {
            if ((new TYUTGB023S()).ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
            }
        }
        #endregion

        #region Description : 출고유형 이벤트
        private void CBO01_JIWKTYPE_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (this.CBO01_JIWKTYPE.GetValue().ToString() != "01")
                {
                    this.CBO01_JISILNUMCK.SetValue("Y");
                }
            }
        }

        private void CBO01_JIWKTYPE_Leave(object sender, EventArgs e)
        {
            if (this.CBO01_JIWKTYPE.GetValue().ToString() != "01")
            {
                this.CBO01_JISILNUMCK.SetValue("Y");
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

        #region Description : 지시 시간 입력시 특허구분 자동 입력
        private void TXT01_JITIMEHH_TextChanged(object sender, EventArgs e)
        {
            string sGUBN = string.Empty;
            double dMMDD;
            double dDay;

            if (this.TXT01_JITIMEHH.GetValue().ToString().Length == 2)
            {
                if (fsRunCheck == "RUN")
                {
                    fsRunCheck = "";
                }
                else
                {
                    // 근태월력 조회 (1 : 토,일,공휴일 , 2 : 평일) 
                    this.DbConnector.CommandClear();

                    this.DbConnector.Attach
                        (
                        "TY_P_UT_A7OF1817",
                        Get_Date(this.DTP01_JIYYMM.GetValue().ToString())
                        );

                    DataTable dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        sGUBN = dt.Rows[0]["WEEK"].ToString();
                        dMMDD = Convert.ToDouble(Get_Date(this.DTP01_JIYYMM.GetValue().ToString()).Substring(4, 4));
                        dDay = Convert.ToDouble(Get_Numeric(TXT01_JITIMEHH.GetValue().ToString()));

                        if (dDay == 00)
                        {
                            dDay = 24;
                        }

                        if (sGUBN == "1")
                        {
                            if (dDay >= 09 && dDay < 24)   // 특허
                            {
                                this.CBO01_JITMGUBN.SetValue("3");
                            }
                            else // 조출
                            {
                                this.CBO01_JITMGUBN.SetValue("2");
                            }
                        }
                        else
                        {
                            if (dMMDD >= 0401 && dMMDD <= 1031) // 하절기
                            {
                                if (dDay >= 09 && dDay < 18)    // 일반
                                {
                                    this.CBO01_JITMGUBN.SetValue("1");
                                }
                                else if (dDay >= 18 && dDay < 24)   // 특허
                                {
                                    this.CBO01_JITMGUBN.SetValue("3");
                                }
                                else // 조출
                                {
                                    this.CBO01_JITMGUBN.SetValue("2");
                                }
                            }
                            else // 동절기
                            {
                                if (dDay >= 09 && dDay < 17)    // 일반
                                {
                                    this.CBO01_JITMGUBN.SetValue("1");
                                }
                                else if (dDay >= 17 && dDay < 24)   // 특허
                                {
                                    this.CBO01_JITMGUBN.SetValue("3");
                                }
                                else // 조출
                                {
                                    this.CBO01_JITMGUBN.SetValue("2");
                                }
                            }
                        }
                    }
                }
            }
        }
        #endregion

        #region Description : 화주 입력시 GS-PI NO 필드 잠금
        private void TXT01_JIJGHWAJU_TextChanged(object sender, EventArgs e)
        {
            if (this.TXT01_JIJGHWAJU.GetValue().ToString() == "GSG")
            {
                this.TXT01_JIGSPINO.SetReadOnly(false);
            }
            else
            {
                this.TXT01_JIGSPINO.SetReadOnly(true);
                this.TXT01_JIGSPINO.SetValue("");
            }
        }
        #endregion

        //#region Description : 오더 버튼 이벤트
        //private void BTN61_ORDER_Click(object sender, EventArgs e)
        //{
        //    string sp = this.IPAdresss + Employer.EmpNo;

        //    TYUTGB013S popup = new TYUTGB013S();

        //    if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
        //    {
        //        this.DTP01_ORDATE.SetValue(popup.fsORDATE);
        //        this.TXT01_ORSEQ.SetValue(popup.fsORSEQ);

        //        UP_Get_UTIORDERF();
        //    }
        //}

        //private void DTP01_ORDATE_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == System.Windows.Forms.Keys.F1)
        //    {
        //        TYUTGB013S popup = new TYUTGB013S();

        //        if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
        //        {
        //            this.DTP01_ORDATE.SetValue(popup.fsORDATE);
        //            this.TXT01_ORSEQ.SetValue(popup.fsORSEQ);

        //            UP_Get_UTIORDERF();
        //        }
        //    }
        //}

        //private void TXT01_ORSEQ_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == System.Windows.Forms.Keys.F1)
        //    {
        //        TYUTGB013S popup = new TYUTGB013S();

        //        if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
        //        {
        //            this.DTP01_ORDATE.SetValue(popup.fsORDATE);
        //            this.TXT01_ORSEQ.SetValue(popup.fsORSEQ);

        //            UP_Get_UTIORDERF();
        //        }
        //    }
        //}

        //private void UP_Get_UTIORDERF()
        //{
        //    DataTable dt = new DataTable();

        //    this.DbConnector.CommandClear();
        //    this.DbConnector.Attach
        //        (
        //        "TY_P_UT_6AHHE388",
        //        Get_Date(this.DTP01_ORDATE.GetValue().ToString()), // 오더일자
        //        this.TXT01_ORSEQ.GetValue().ToString()             // 순번
        //        );

        //    dt = this.DbConnector.ExecuteDataTable();

        //    if (dt.Rows.Count > 0)
        //    {
        //        this.CurrentDataTableRowMapping(dt, "01");
        //    }

        //    // 이전 HSN에 대한 데이터 가져오기
        //    UP_GET_PreIPMTQTY();
        //}
        //#endregion
    }
}