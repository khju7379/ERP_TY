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
    public partial class TYUTIN006I : TYBase
    {
        string fsCHTKNO = string.Empty;

        string fsCHIPHANG = string.Empty;
        string fsCHBONSUN = string.Empty;
        string fsCHHWAJU = string.Empty;
        string fsCHHWAMUL = string.Empty;
        string fsCHBLNO = string.Empty;
        string fsCHMSNSEQ = string.Empty;
        string fsCHHSNSEQ = string.Empty;
        string fsCHACTHJ = string.Empty;
        string fsCHCUSTIL = string.Empty;
        string fsCHCHASU = string.Empty;
        string fsCHCHTANK = string.Empty;
        string fsCHJGHWAJU = string.Empty;
        string fsCHYSHWAJU = string.Empty;
        string fsCHYDHWAJU = string.Empty;
        string fsCHYSDATE = string.Empty;
        string fsCHYDSEQ = string.Empty;
        string fsCHYSSEQ = string.Empty;
        string fsCHIPTANK = string.Empty;
        string fsCHYNGUBUN = string.Empty;
        string fsCHYNCHQTY_AFT = string.Empty;
        string fsCHYNCHQTY_AGO = string.Empty;

        string fsSVMTQTY = string.Empty;
        string fsSVCHQTY = string.Empty;
        string fsSVKLQTY = string.Empty;
        string fsSVBIJUNG = string.Empty;

        string fsKLQTY = string.Empty;
        string fsMTQTY = string.Empty;

        string fsDJCHQTY = string.Empty;
        string fsDJPOQTY = string.Empty;
        string fsDJJEQTY = string.Empty;

        string fsDTCHQTY = string.Empty;
        string fsDTPOQTY = string.Empty;
        string fsDTJEQTY = string.Empty;

        string fsCONVERT = string.Empty;
        string fsOVER_KL = string.Empty;
        string fsKESAN = string.Empty;
        string fsCHHWAPE = string.Empty;
        string fsCHMTQTY = string.Empty;
        string fsCHKLQTY = string.Empty;
        string fsCHJISINUM = string.Empty;
        string fsCHQTY = string.Empty;

        string fsCHJUNG = string.Empty;
        string fsCHDRQTY = string.Empty;
        string fsCHCHHJ = string.Empty;
        string fsCHBIJUNG = string.Empty;
        string fsCHVCF = string.Empty;
        string fsSCCHJEQTY = string.Empty;
        string fsSCIPJEQTY = string.Empty;
        string fsCHDANYI = string.Empty;

        string fsCHCHULGB = string.Empty;

        string fsCHHIGB = string.Empty;

        string fsPOPUP = string.Empty;

        private string fsGUBUN = string.Empty;

        private string fsJDJIQTY = string.Empty;

        #region Description : 페이지 로드
        public TYUTIN006I()
        {
            InitializeComponent();
        }

        public TYUTIN006I(string sCHCHULIL, string sCHTKNO)
        {
            InitializeComponent();

            this.DTP01_CHCHULIL.SetValue(Set_Date(sCHCHULIL.ToString()));
            this.TXT01_CHTKNO.SetValue(sCHTKNO.ToString());

            fsPOPUP = "POPUP";
        }

        private void TYUTIN006I_Load(object sender, System.EventArgs e)
        {
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            UP_BUTTON_Visible("");

            UP_FieldClear("LOAD");

            this.SetStartingFocus(this.DTP01_STIPHANG);

            //UP_Label_Color();

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
            UP_FieldClear("NEW");

            UP_Set_ReadOnly("NEW");

            UP_BUTTON_Visible("NEW");

            fsGUBUN = "NEW";

            this.CBH01_CHCHHASAB.SetValue(TYUserInfo.EmpNo.ToString().Trim().ToUpper());

            SetFocus(this.TXT01_CHJGHWAJU);
        }
        #endregion

        #region Description : 저장 버튼
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            string sCHCHSTR = string.Empty;
            string sCHCHEND = string.Empty;
            string sCHOVSTR = string.Empty;
            string sCHOVEND = string.Empty;
            string sCHJISINUM = string.Empty;


            string sCHDRQTY_AFT = string.Empty;
            string sCHMTQTY_AFT = string.Empty;
            string sCHKLQTY_AFT = string.Empty;

            string sDRUC_GUBUN = string.Empty;
            string sCHNU_GUBUN = string.Empty;
            string sJIDR_GUBUN = string.Empty;
            string sDRUJ_GUBUN = string.Empty;
            string sDRUT_GUBUN = string.Empty;

            string sJCSEQ = string.Empty;

            DataTable dt = new DataTable();

            sDRUC_GUBUN = "";

            // 일자별 DRUM 출고 파일 체크
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_6CLHF155",
                Get_Date(this.DTP01_CHCHULIL.GetValue().ToString().Trim()),
                this.CBH01_CHACTHJ.GetValue().ToString().Trim(),
                this.CBH01_CHHWAMUL.GetValue().ToString().Trim(),
                Get_Date(this.DTP01_CHIPHANG.GetValue().ToString().Trim()),
                this.CBH01_CHBONSUN.GetValue().ToString().Trim(),
                this.CBH01_CHHWAJU.GetValue().ToString().Trim(),
                this.TXT01_CHBLNO.GetValue().ToString().Trim(),
                Get_Numeric(this.TXT01_CHMSNSEQ.GetValue().ToString().Trim()),
                Get_Numeric(this.TXT01_CHHSNSEQ.GetValue().ToString().Trim()),
                Get_Date(this.DTP01_CHCUSTIL.GetValue().ToString().Trim()),
                Get_Numeric(this.TXT01_CHCHASU.GetValue().ToString().Trim()),
                this.TXT01_CHJGHWAJU.GetValue().ToString(),
                this.CBH01_CHYSHWAJU.GetValue().ToString(),
                this.CBH01_CHYDHWAJU.GetValue().ToString(),
                Get_Date(this.DTP01_CHYSDATE.GetValue().ToString().Trim()),
                this.TXT01_CHYDSEQ.GetValue().ToString(),
                this.TXT01_CHYSSEQ.GetValue().ToString(),
                Get_Numeric(this.TXT01_CHJUNG.GetValue().ToString().Trim())
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count <= 0)
            {
                sDRUC_GUBUN = "INS";
            }
            else
            {
                sDRUC_GUBUN = "UPT";
            }


            // 출고 누계 체크
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_6CNGT202",
                Get_Date(this.DTP01_CHCHULIL.GetValue().ToString().Trim()).Substring(0, 4).ToString(),
                Get_Date(this.DTP01_CHCHULIL.GetValue().ToString().Trim()).Substring(4, 2).ToString(),
                this.CBH01_CHHWAJU.GetValue().ToString().Trim(),
                this.CBH01_CHHWAMUL.GetValue().ToString().Trim(),
                this.CBH01_CHACTHJ.GetValue().ToString().Trim(),
                this.CBH01_CHCHHJ.GetValue().ToString().Trim()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count <= 0)
            {
                sCHNU_GUBUN = "INS";
            }
            else
            {
                sCHNU_GUBUN = "UPT";
            }

            // DRUM 지시 체크
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_6CNHM203",
                Get_Date(this.DTP01_CHIPHANG.GetValue().ToString().Trim()),
                this.CBH01_CHBONSUN.GetValue().ToString().Trim().ToUpper(),
                this.CBH01_CHHWAJU.GetValue().ToString().Trim().ToUpper(),
                this.CBH01_CHHWAMUL.GetValue().ToString().Trim().ToUpper(),
                this.TXT01_CHBLNO.GetValue().ToString().Trim(),
                Get_Numeric(this.TXT01_CHMSNSEQ.GetValue().ToString().Trim()),
                Get_Numeric(this.TXT01_CHHSNSEQ.GetValue().ToString().Trim()),
                this.CBH01_CHACTHJ.GetValue().ToString().Trim().ToUpper(),
                Get_Date(this.DTP01_CHCUSTIL.GetValue().ToString().Trim()),
                Get_Numeric(this.TXT01_CHCHASU.GetValue().ToString().Trim()),
                this.TXT01_CHJGHWAJU.GetValue().ToString(),
                this.CBH01_CHYSHWAJU.GetValue().ToString(),
                this.CBH01_CHYDHWAJU.GetValue().ToString(),
                Get_Date(this.DTP01_CHYSDATE.GetValue().ToString().Trim()),
                this.TXT01_CHYDSEQ.GetValue().ToString(),
                this.TXT01_CHYSSEQ.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                sJIDR_GUBUN = "UPT";
            }

            // DRUM 재고 체크
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_6CLGO147",
                this.CBH01_CHACTHJ.GetValue().ToString().Trim(),
                this.CBH01_CHHWAMUL.GetValue().ToString().Trim(),
                Get_Date(this.DTP01_CHIPHANG.GetValue().ToString().Trim()),
                this.CBH01_CHBONSUN.GetValue().ToString().Trim(),
                this.CBH01_CHHWAJU.GetValue().ToString().Trim(),
                this.TXT01_CHBLNO.GetValue().ToString().Trim(),
                Get_Numeric(this.TXT01_CHMSNSEQ.GetValue().ToString().Trim()),
                Get_Numeric(this.TXT01_CHHSNSEQ.GetValue().ToString().Trim()),
                Get_Date(this.DTP01_CHCUSTIL.GetValue().ToString().Trim()),
                Get_Numeric(this.TXT01_CHCHASU.GetValue().ToString().Trim()),
                this.TXT01_CHJGHWAJU.GetValue().ToString(),
                this.CBH01_CHYSHWAJU.GetValue().ToString(),
                this.CBH01_CHYDHWAJU.GetValue().ToString(),
                Get_Date(this.DTP01_CHYSDATE.GetValue().ToString().Trim()),
                this.TXT01_CHYDSEQ.GetValue().ToString(),
                this.TXT01_CHYSSEQ.GetValue().ToString(),
                Get_Numeric(this.TXT01_CHJUNG.GetValue().ToString().Trim())
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                sDRUJ_GUBUN = "UPT";
            }

            // 출고 - 탱크별 DRUM 재고 체크
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_6CLH8151",
                this.CBH01_CHACTHJ.GetValue().ToString().Trim(),
                this.CBH01_CHHWAMUL.GetValue().ToString().Trim(),
                Get_Date(this.DTP01_CHIPHANG.GetValue().ToString().Trim()),
                this.CBH01_CHBONSUN.GetValue().ToString().Trim(),
                this.CBH01_CHHWAJU.GetValue().ToString().Trim(),
                this.TXT01_CHCHTANK.GetValue().ToString().Trim(),
                Get_Numeric(this.TXT01_CHJUNG.GetValue().ToString().Trim())
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                sDRUT_GUBUN = "UPT";
            }

            // 20180110 김종술 과장 출하구분이 송유일 경우 출하유형 BLUK로 되도록 요청
            if (this.CBH01_CHCHULGB.GetValue().ToString() == "05")
            {
                this.CBO01_CHWKTYPE.SetValue("06");
            }



            if (fsGUBUN == "NEW")
            {
                #region Description : 저장

                string sCHCHULGB = string.Empty;

                if (this.CBH01_CHCHULGB.GetValue().ToString() == "01")
                {
                    sCHCHULGB = "01";
                }
                else
                {
                    if (this.CBH01_CHCHULGB.GetValue().ToString() == "02" || this.CBH01_CHCHULGB.GetValue().ToString() == "09" || this.CBH01_CHCHULGB.GetValue().ToString() == "10")
                    {
                        sCHCHULGB = "02";
                    }
                    else
                    {
                        sCHCHULGB = this.CBH01_CHCHULGB.GetValue().ToString();
                    }

                }

                // 출고순번 가져오기
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_UT_6CMB2159",
                    sCHCHULGB.ToString(),
                    sCHCHULGB.ToString(),
                    sCHCHULGB.ToString(),
                    sCHCHULGB.ToString(),
                    sCHCHULGB.ToString(),
                    Get_Date(this.DTP01_CHCHULIL.GetValue().ToString()),
                    sCHCHULGB.ToString()
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.TXT01_CHTKNO.SetValue(dt.Rows[0]["CATKNO"].ToString());
                }

                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_UT_7AHG2829",
                    Get_Date(this.DTP01_CHCHULIL.GetValue().ToString()),
                    sCHCHULGB.ToString()
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    // 출고지시번호 업데이트
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_UT_6CNEB199", this.TXT01_CHTKNO.GetValue().ToString(),
                                                                Get_Date(this.DTP01_CHCHULIL.GetValue().ToString()),
                                                                sCHCHULGB.ToString()
                                                                );

                    this.DbConnector.ExecuteNonQuery();

                }
                else
                {
                    // 출고지시번호 등록
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_UT_6CNE5198", Get_Date(this.DTP01_CHCHULIL.GetValue().ToString()),
                                                                sCHCHULGB.ToString(),
                                                                this.TXT01_CHTKNO.GetValue().ToString()
                                                                );

                    this.DbConnector.ExecuteNonQuery();
                }

                sCHCHSTR = Set_Fill2(this.TXT01_CHCHSTR1.GetValue().ToString()) + Set_Fill2(this.TXT01_CHCHSTR2.GetValue().ToString());
                sCHCHEND = Set_Fill2(this.TXT01_CHCHEND1.GetValue().ToString()) + Set_Fill2(this.TXT01_CHCHEND2.GetValue().ToString());
                sCHOVSTR = Set_Fill2(this.TXT01_CHOVSTR1.GetValue().ToString()) + Set_Fill2(this.TXT01_CHOVSTR2.GetValue().ToString());
                sCHOVEND = Set_Fill2(this.TXT01_CHOVEND1.GetValue().ToString()) + Set_Fill2(this.TXT01_CHOVEND2.GetValue().ToString());

                sCHJISINUM = Get_Date(this.TXT01_CHJISINUM1.GetValue().ToString()) + Set_Fill3(TXT01_CHJISINUM2.GetValue().ToString());

                #region Description : 출고관리


                fsCHYNGUBUN = "";
                fsCHYNCHQTY_AFT = "0";

                if (this.CBH01_CHYSHWAJU.GetValue().ToString() != "" && this.CBH01_CHYDHWAJU.GetValue().ToString() != "" &&
                   Get_Date(this.DTP01_CHYSDATE.GetValue().ToString().Trim()) != "0" && Get_Numeric(this.TXT01_CHYDSEQ.GetValue().ToString()) != "0" &&
                   Get_Numeric(this.TXT01_CHYSSEQ.GetValue().ToString()) != "0")
                {
                    fsCHYNGUBUN = "R";

                    fsCHYNCHQTY_AFT = this.TXT01_CHMTQTY.GetValue().ToString();
                }

                this.DbConnector.CommandClear();

                this.DbConnector.Attach("TY_P_UT_83CGX690", Get_Date(this.DTP01_CHIPHANG.GetValue().ToString()),   // 입항일자
                                                            this.CBH01_CHBONSUN.GetValue().ToString(),             // 본선
                                                            this.CBH01_CHHWAJU.GetValue().ToString(),              // 화주
                                                            this.CBH01_CHHWAMUL.GetValue().ToString(),             // 화물
                                                            this.TXT01_CHBLNO.GetValue().ToString(),               // BL번호
                                                            this.TXT01_CHMSNSEQ.GetValue().ToString(),             // MSN번호
                                                            this.TXT01_CHHSNSEQ.GetValue().ToString(),             // HSN번호
                                                            Get_Date(this.DTP01_CHCUSTIL.GetValue().ToString()),   // 통관일자
                                                            this.TXT01_CHCHASU.GetValue().ToString(),              // 통관차수
                                                            this.CBH01_CHACTHJ.GetValue().ToString(),              // 통관화주
                                                            this.TXT01_CHJGHWAJU.GetValue().ToString(),            // 재고화주
                                                            this.CBH01_CHYSHWAJU.GetValue().ToString(),            // 양수화주
                                                            this.CBH01_CHYDHWAJU.GetValue().ToString(),            // 양도화주
                                                            Get_Date(this.DTP01_CHYSDATE.GetValue().ToString()),   // 양수일자
                                                            this.TXT01_CHYDSEQ.GetValue().ToString(),              // 양도차수
                                                            this.TXT01_CHYSSEQ.GetValue().ToString(),              // 양수순번
                                                            Get_Date(this.DTP01_CHCHULIL.GetValue().ToString()),   // 출고일자
                                                            this.TXT01_CHTKNO.GetValue().ToString(),               // 순번
                                                            fsCHYNGUBUN.ToString(),                                // 양수구분
                                                            fsCHYNCHQTY_AFT.ToString(),                            // 양수출고량
                                                            this.CBH01_CHCHHJ.GetValue().ToString(),               // 출고화주
                                                            Get_Date(this.DTP01_CHENDIL.GetValue().ToString()),    // 출고종료일
                                                            Set_TankNo(this.TXT01_CHCHTANK.GetValue().ToString()), // 출고탱크
                                                            Set_TankNo(this.TXT01_CHIPTANK.GetValue().ToString()), // 입고탱크
                                                            this.TXT01_CHCONTNO.GetValue().ToString(),             // 계약번호
                                                            this.CBH01_CHCHULGB.GetValue().ToString(),             // 출하구분
                                                            this.CBO01_CHWKTYPE.GetValue().ToString(),             // 작업방법
                                                            sCHCHSTR.ToString(),
                                                            sCHCHEND.ToString(),
                                                            this.CBH01_CHCHJANG.GetValue().ToString(),
                                                            Get_Numeric(this.TXT01_CHMTQTY.GetValue().ToString()),
                                                            Get_Numeric(this.TXT01_CHKLQTY.GetValue().ToString()),
                                                            this.CBO01_CHDANYI.GetValue().ToString(),
                                                            this.TXT01_CHQTY.GetValue().ToString(),
                                                            fsCHBIJUNG.ToString(),
                                                            fsCHVCF.ToString(),
                                                            sCHOVSTR.ToString(),
                                                            sCHOVEND.ToString(),
                                                            this.TXT01_CHOVQTY.GetValue().ToString(),
                                                            this.TXT01_CHOVAM.GetValue().ToString(),
                                                            this.CBH01_CHSOSOK.GetValue().ToString(),
                                                            this.CBH01_CHCHHASAB.GetValue().ToString(),
                                                            this.CBH01_CHCHSAB.GetValue().ToString(),
                                                            Get_Date(this.DTP01_CHSUCHIP.GetValue().ToString()),
                                                            this.CBH01_CHSUCHVS.GetValue().ToString(),
                                                            fsCHHWAPE.ToString(),
                                                            this.TXT01_CHJUNG.GetValue().ToString(),
                                                            sCHJISINUM.ToString(),
                                                            this.CBH01_CHJISAB.GetValue().ToString(),
                                                            this.TXT01_CHEMPTY.GetValue().ToString(),
                                                            this.TXT01_CHTOTAL.GetValue().ToString(),
                                                            this.TXT01_CHCARNO.GetValue().ToString(),
                                                            this.CBH01_CHOUTCHSAB.GetValue().ToString(),
                                                            this.TXT01_CHCONTNUM.GetValue().ToString(),
                                                            this.TXT01_CHSILNUM.GetValue().ToString(),
                                                            this.CBH01_CHDNST.GetValue().ToString(),
                                                            TYUserInfo.EmpNo.ToString().Trim().ToUpper()         // 작성사번
                                                            );

                #endregion

                #region Description : 출고누계

                if (sCHNU_GUBUN == "INS")
                {
                    // 출고누계 등록
                    this.DbConnector.Attach("TY_P_UT_6CQE6214", Get_Date(this.DTP01_CHCHULIL.GetValue().ToString().Trim()).Substring(0, 4).ToString(),
                                                                Get_Date(this.DTP01_CHCHULIL.GetValue().ToString().Trim()).Substring(4, 2).ToString(),
                                                                this.CBH01_CHHWAJU.GetValue().ToString().Trim(),
                                                                this.CBH01_CHHWAMUL.GetValue().ToString().Trim(),
                                                                this.CBH01_CHACTHJ.GetValue().ToString().Trim(),
                                                                this.CBH01_CHCHHJ.GetValue().ToString().Trim(),
                                                                Get_Numeric(this.TXT01_CHMTQTY.GetValue().ToString())
                                                                );

                }
                else
                {
                    // 출고누계 업데이트
                    this.DbConnector.Attach("TY_P_UT_6CQE1216", "0",                                                   // 이전 출고량
                                                                Get_Numeric(this.TXT01_CHMTQTY.GetValue().ToString()), // 현 출고량
                                                                Get_Date(this.DTP01_CHCHULIL.GetValue().ToString().Trim()).Substring(0, 4).ToString(),
                                                                Get_Date(this.DTP01_CHCHULIL.GetValue().ToString().Trim()).Substring(4, 2).ToString(),
                                                                this.CBH01_CHHWAJU.GetValue().ToString().Trim(),
                                                                this.CBH01_CHHWAMUL.GetValue().ToString().Trim(),
                                                                this.CBH01_CHACTHJ.GetValue().ToString().Trim(),
                                                                this.CBH01_CHCHHJ.GetValue().ToString().Trim()
                                                                );

                }

                #endregion

                #region Description : 매출입고 할증

                this.DbConnector.Attach("TY_P_UT_6CQEH217", "0",                                                   // 이전 출고량
                                                            Get_Numeric(this.TXT01_CHMTQTY.GetValue().ToString()), // 현 출고량
                                                            Get_Date(this.DTP01_CHIPHANG.GetValue().ToString()),   // 입항일자
                                                            this.CBH01_CHBONSUN.GetValue().ToString(),             // 본선
                                                            this.CBH01_CHHWAJU.GetValue().ToString(),              // 화주
                                                            this.CBH01_CHHWAMUL.GetValue().ToString(),             // 화물
                                                            this.TXT01_CHIPTANK.GetValue().ToString().Trim()       // 입고탱크
                                                            );

                #endregion

                #region Description : SURVEY 파일

                if (this.CBO01_CHDANYI.GetValue().ToString() != "2")
                {
                    this.DbConnector.Attach("TY_P_UT_6CQEK218", "0",                                                   // 이전 출고량
                                                                Get_Numeric(this.TXT01_CHMTQTY.GetValue().ToString()), // 현 출고량
                                                                Get_Date(this.DTP01_CHIPHANG.GetValue().ToString()),   // 입항일자
                                                                this.CBH01_CHBONSUN.GetValue().ToString(),             // 본선
                                                                this.CBH01_CHHWAJU.GetValue().ToString(),              // 화주
                                                                this.CBH01_CHHWAMUL.GetValue().ToString(),             // 화물
                                                                this.TXT01_CHCHTANK.GetValue().ToString().Trim()       // 출고탱크
                                                                );

                }

                #endregion

                #region Description : B/L별 입고파일

                this.DbConnector.Attach("TY_P_UT_6CQES219", "0",                                                   // 이전 출고량
                                                            Get_Numeric(this.TXT01_CHMTQTY.GetValue().ToString()), // 현 출고량
                                                            Get_Date(this.DTP01_CHIPHANG.GetValue().ToString()),   // 입항일자
                                                            this.CBH01_CHBONSUN.GetValue().ToString(),             // 본선
                                                            this.CBH01_CHHWAJU.GetValue().ToString(),              // 화주
                                                            this.CBH01_CHHWAMUL.GetValue().ToString(),             // 화물
                                                            this.TXT01_CHBLNO.GetValue().ToString(),               // BL번호
                                                            this.TXT01_CHMSNSEQ.GetValue().ToString(),             // MSN번호
                                                            this.TXT01_CHHSNSEQ.GetValue().ToString()              // HSN번호
                                                            );

                #endregion

                #region Description : 통관파일

                this.DbConnector.Attach("TY_P_UT_6CQEX220", "0",                                                   // 이전 출고량
                                                            Get_Numeric(this.TXT01_CHMTQTY.GetValue().ToString()), // 현 출고량
                                                            Get_Date(this.DTP01_CHIPHANG.GetValue().ToString()),   // 입항일자
                                                            this.CBH01_CHBONSUN.GetValue().ToString(),             // 본선
                                                            this.CBH01_CHHWAJU.GetValue().ToString(),              // 화주
                                                            this.CBH01_CHHWAMUL.GetValue().ToString(),             // 화물
                                                            this.TXT01_CHBLNO.GetValue().ToString(),               // BL번호
                                                            this.TXT01_CHMSNSEQ.GetValue().ToString(),             // MSN번호
                                                            this.TXT01_CHHSNSEQ.GetValue().ToString(),             // HSN번호
                                                            Get_Date(this.DTP01_CHCUSTIL.GetValue().ToString()),   // 통관일자
                                                            this.TXT01_CHCHASU.GetValue().ToString()               // 통관차수
                                                            );

                #endregion

                #region Description : 통관화주별 재고 파일

                string sCHMTQTY = string.Empty;

                sCHMTQTY = "0";
                fsCHYNCHQTY_AFT = "0";

                if (this.CBH01_CHYSHWAJU.GetValue().ToString() != "" && this.CBH01_CHYDHWAJU.GetValue().ToString() != "" &&
                   Get_Date(this.DTP01_CHYSDATE.GetValue().ToString().Trim()) != "0" && Get_Numeric(this.TXT01_CHYDSEQ.GetValue().ToString()) != "0" &&
                   Get_Numeric(this.TXT01_CHYSSEQ.GetValue().ToString()) != "0")
                {
                    fsCHYNGUBUN = "R";

                    fsCHYNCHQTY_AFT = this.TXT01_CHMTQTY.GetValue().ToString();
                }
                else
                {
                    sCHMTQTY = Get_Numeric(this.TXT01_CHMTQTY.GetValue().ToString());
                }

                this.DbConnector.Attach("TY_P_UT_6CQF8221", "0",                                                   // 이전 양수출고량
                                                            fsCHYNCHQTY_AFT,                                       // 이후 양수출고량
                                                            "0",                                                   // 이전 출고량
                                                            sCHMTQTY,                                              // 현   출고량
                                                            "0",                                                   // 이전 양수출고량
                                                            "0",                                                   // 이전 출고량
                                                            fsCHYNCHQTY_AFT,                                       // 이후 양수출고량
                                                            sCHMTQTY,                                              // 현   출고량
                                                            this.CBH01_CHACTHJ.GetValue().ToString(),              // 통관화주
                                                            this.TXT01_CHJGHWAJU.GetValue().ToString(),            // 재고화주
                                                            Get_Date(this.DTP01_CHIPHANG.GetValue().ToString()),   // 입항일자
                                                            this.CBH01_CHBONSUN.GetValue().ToString(),             // 본선
                                                            this.CBH01_CHHWAJU.GetValue().ToString(),              // 화주
                                                            this.CBH01_CHHWAMUL.GetValue().ToString(),             // 화물
                                                            this.TXT01_CHBLNO.GetValue().ToString(),               // BL번호
                                                            this.TXT01_CHMSNSEQ.GetValue().ToString(),             // MSN번호
                                                            this.TXT01_CHHSNSEQ.GetValue().ToString(),             // HSN번호
                                                            Get_Date(this.DTP01_CHCUSTIL.GetValue().ToString()),   // 통관일자
                                                            this.TXT01_CHCHASU.GetValue().ToString(),              // 통관차수
                                                            this.CBH01_CHYSHWAJU.GetValue().ToString(),            // 양수화주
                                                            this.CBH01_CHYDHWAJU.GetValue().ToString(),            // 양도화주
                                                            Get_Date(this.DTP01_CHYSDATE.GetValue().ToString()),   // 양수일자
                                                            this.TXT01_CHYDSEQ.GetValue().ToString(),              // 양도차수
                                                            this.TXT01_CHYSSEQ.GetValue().ToString()               // 양수순번
                                                            );

                #endregion

                #region Description : 양수도 파일

                if (this.CBH01_CHYSHWAJU.GetValue().ToString() != "" && this.CBH01_CHYDHWAJU.GetValue().ToString() != "" &&
                   Get_Date(this.DTP01_CHYSDATE.GetValue().ToString().Trim()) != "0" && Get_Numeric(this.TXT01_CHYDSEQ.GetValue().ToString()) != "0" &&
                   Get_Numeric(this.TXT01_CHYSSEQ.GetValue().ToString()) != "0")
                {
                    this.DbConnector.Attach("TY_P_UT_6CQFZ222", "0",                                                   // 이전 양수출고량
                                                                fsCHYNCHQTY_AFT,                                       // 이후 양수출고량
                                                                "0",                                                   // 이전 양수출고량
                                                                fsCHYNCHQTY_AFT,                                       // 이후 양수출고량
                                                                Get_Date(this.DTP01_CHIPHANG.GetValue().ToString()),   // 입항일자
                                                                this.CBH01_CHBONSUN.GetValue().ToString(),             // 본선
                                                                this.CBH01_CHHWAJU.GetValue().ToString(),              // 화주
                                                                this.CBH01_CHHWAMUL.GetValue().ToString(),             // 화물
                                                                this.TXT01_CHBLNO.GetValue().ToString(),               // BL번호
                                                                this.TXT01_CHMSNSEQ.GetValue().ToString(),             // MSN번호
                                                                this.TXT01_CHHSNSEQ.GetValue().ToString(),             // HSN번호
                                                                Get_Date(this.DTP01_CHCUSTIL.GetValue().ToString()),   // 통관일자
                                                                this.TXT01_CHCHASU.GetValue().ToString(),              // 통관차수
                                                                this.CBH01_CHACTHJ.GetValue().ToString(),              // 통관화주
                                                                this.CBH01_CHYDHWAJU.GetValue().ToString(),            // 양도화주
                                                                this.CBH01_CHYSHWAJU.GetValue().ToString(),            // 양수화주
                                                                Get_Date(this.DTP01_CHYSDATE.GetValue().ToString()),   // 양수일자
                                                                this.TXT01_CHYDSEQ.GetValue().ToString(),              // 양도차수
                                                                this.TXT01_CHYSSEQ.GetValue().ToString()               // 양수순번
                                                                );
                }

                #endregion

                #region Description : 일자별 DRUM 출고파일

                if (this.CBO01_CHDANYI.GetValue().ToString() == "2")
                {
                    sCHDRQTY_AFT = this.TXT01_CHQTY.GetValue().ToString();
                    fsCHDRQTY = "0";

                    if (sDRUC_GUBUN == "INS") // 등록
                    {
                        // 일자별 DRUM 출고파일 등록
                        this.DbConnector.Attach("TY_P_UT_6CQD0208", Get_Date(this.DTP01_CHCHULIL.GetValue().ToString()),   // 출고일자
                                                                    this.CBH01_CHACTHJ.GetValue().ToString(),              // 통관화주
                                                                    Get_Date(this.DTP01_CHIPHANG.GetValue().ToString()),   // 입항일자
                                                                    this.CBH01_CHBONSUN.GetValue().ToString(),             // 본선
                                                                    this.CBH01_CHHWAJU.GetValue().ToString(),              // 화주
                                                                    this.CBH01_CHHWAMUL.GetValue().ToString(),             // 화물
                                                                    this.TXT01_CHBLNO.GetValue().ToString(),               // BL번호
                                                                    this.TXT01_CHMSNSEQ.GetValue().ToString(),             // MSN번호
                                                                    this.TXT01_CHHSNSEQ.GetValue().ToString(),             // HSN번호
                                                                    Get_Date(this.DTP01_CHCUSTIL.GetValue().ToString()),   // 통관일자
                                                                    this.TXT01_CHCHASU.GetValue().ToString(),              // 통관차수
                                                                    this.TXT01_CHJGHWAJU.GetValue().ToString(),            // 재고화주
                                                                    this.CBH01_CHYSHWAJU.GetValue().ToString(),            // 양수화주
                                                                    this.CBH01_CHYDHWAJU.GetValue().ToString(),            // 양도화주
                                                                    Get_Date(this.DTP01_CHYSDATE.GetValue().ToString()),   // 양수일자
                                                                    this.TXT01_CHYDSEQ.GetValue().ToString(),              // 양도차수
                                                                    this.TXT01_CHYSSEQ.GetValue().ToString(),              // 양수순번
                                                                    this.TXT01_CHJUNG.GetValue().ToString(),               // 중량
                                                                    sCHDRQTY_AFT.ToString(),                               // 드럼개수
                                                                    Get_Numeric(this.TXT01_CHMTQTY.GetValue().ToString()), // MT량
                                                                    Get_Numeric(this.TXT01_CHKLQTY.GetValue().ToString()), // KL량
                                                                    TYUserInfo.EmpNo.ToString().Trim().ToUpper()           // 작성사번
                                                                    );
                    }
                    else // 수정
                    {
                        // 일자별 DRUM 출고파일 업데이트
                        this.DbConnector.Attach("TY_P_UT_6CQCY207", "0",                                                   // 이전 드럼개수
                                                                    sCHDRQTY_AFT.ToString(),                               // 이후 드럼개수
                                                                    "0",                                                   // 이전 MT량
                                                                    Get_Numeric(this.TXT01_CHMTQTY.GetValue().ToString()), // 이후 MT량
                                                                    "0",                                                   // 이전 KL량                                                                    
                                                                    Get_Numeric(this.TXT01_CHKLQTY.GetValue().ToString()), // 이후 KL량
                                                                    Get_Date(this.DTP01_CHCHULIL.GetValue().ToString()),   // 출고일자
                                                                    this.CBH01_CHACTHJ.GetValue().ToString(),              // 통관화주
                                                                    Get_Date(this.DTP01_CHIPHANG.GetValue().ToString()),   // 입항일자
                                                                    this.CBH01_CHBONSUN.GetValue().ToString(),             // 본선
                                                                    this.CBH01_CHHWAJU.GetValue().ToString(),              // 화주
                                                                    this.CBH01_CHHWAMUL.GetValue().ToString(),             // 화물
                                                                    this.TXT01_CHBLNO.GetValue().ToString(),               // BL번호
                                                                    this.TXT01_CHMSNSEQ.GetValue().ToString(),             // MSN번호
                                                                    this.TXT01_CHHSNSEQ.GetValue().ToString(),             // HSN번호
                                                                    Get_Date(this.DTP01_CHCUSTIL.GetValue().ToString()),   // 통관일자
                                                                    this.TXT01_CHCHASU.GetValue().ToString(),              // 통관차수
                                                                    this.TXT01_CHJGHWAJU.GetValue().ToString(),            // 재고화주
                                                                    this.CBH01_CHYSHWAJU.GetValue().ToString(),            // 양수화주
                                                                    this.CBH01_CHYDHWAJU.GetValue().ToString(),            // 양도화주
                                                                    Get_Date(this.DTP01_CHYSDATE.GetValue().ToString()),   // 양수일자
                                                                    this.TXT01_CHYDSEQ.GetValue().ToString(),              // 양도차수
                                                                    this.TXT01_CHYSSEQ.GetValue().ToString(),              // 양수순번
                                                                    this.TXT01_CHJUNG.GetValue().ToString()                // 중량
                                                            );
                    }
                }

                #endregion

                #region Description : 출고지시 및 DRUM 지시

                // 케미컬 출고일 경우
                if (this.CBO01_CHDANYI.GetValue().ToString() != "2")
                {
                    if (this.TXT01_CHJISINUM1.GetValue().ToString() != "" &&
                        this.TXT01_CHJISINUM2.GetValue().ToString() != "")
                    {
                        // 출고 지시
                        this.DbConnector.Attach("TY_P_UT_6CR9Y228", "0",                                                   // 이전 출고량
                                                                    Get_Numeric(this.TXT01_CHMTQTY.GetValue().ToString()), // 현   출고량
                                                                    "0",                                                   // 이전 출고량
                                                                    Get_Numeric(this.TXT01_CHMTQTY.GetValue().ToString()), // 현   출고량
                                                                    this.TXT01_CHJISINUM1.GetValue().ToString(),           // 지시일자
                                                                    this.TXT01_CHJISINUM2.GetValue().ToString()            // 지시순번
                                                                    );
                    }
                }
                else
                {
                    if (sJIDR_GUBUN == "UPT")
                    {
                        // DRUM 지시
                        this.DbConnector.Attach("TY_P_UT_6CRED233", "0",                                                   // 이전 출고량
                                                                    sCHDRQTY_AFT.ToString(),                               // 현   출고량
                                                                    "0",                                                   // 이전 출고량
                                                                    sCHDRQTY_AFT.ToString(),                               // 현   출고량
                                                                    "0",                                                   // 이전 출고량
                                                                    sCHDRQTY_AFT.ToString(),                               // 현   출고량
                                                                    this.TXT01_CHJISINUM1.GetValue().ToString(),           // 지시일자
                                                                    this.TXT01_CHJISINUM2.GetValue().ToString(),           // 지시순번
                                                                    Get_Date(this.DTP01_CHIPHANG.GetValue().ToString().Trim()),
                                                                    this.CBH01_CHBONSUN.GetValue().ToString().Trim().ToUpper(),
                                                                    this.CBH01_CHHWAJU.GetValue().ToString().Trim().ToUpper(),
                                                                    this.CBH01_CHHWAMUL.GetValue().ToString().Trim().ToUpper(),
                                                                    this.TXT01_CHBLNO.GetValue().ToString().Trim(),
                                                                    Get_Numeric(this.TXT01_CHMSNSEQ.GetValue().ToString().Trim()),
                                                                    Get_Numeric(this.TXT01_CHHSNSEQ.GetValue().ToString().Trim()),
                                                                    this.CBH01_CHACTHJ.GetValue().ToString().Trim().ToUpper(),
                                                                    Get_Date(this.DTP01_CHCUSTIL.GetValue().ToString().Trim()),
                                                                    Get_Numeric(this.TXT01_CHCHASU.GetValue().ToString().Trim()),
                                                                    this.TXT01_CHJGHWAJU.GetValue().ToString(),
                                                                    this.CBH01_CHYSHWAJU.GetValue().ToString(),
                                                                    this.CBH01_CHYDHWAJU.GetValue().ToString(),
                                                                    Get_Date(this.DTP01_CHYSDATE.GetValue().ToString().Trim()),
                                                                    this.TXT01_CHYDSEQ.GetValue().ToString(),
                                                                    this.TXT01_CHYSSEQ.GetValue().ToString()
                                                                    );
                    }
                }

                #endregion

                #region Description : 드럼 출고

                if (this.CBO01_CHDANYI.GetValue().ToString() == "2")
                {
                    if (sDRUJ_GUBUN == "UPT")
                    {
                        // DRUM 재고
                        this.DbConnector.Attach("TY_P_UT_6CREZ234", "0",                                                   // 이전 출고량
                                                                    sCHDRQTY_AFT.ToString(),                               // 현   출고량
                                                                    "0",                                                   // 이전 출고량
                                                                    sCHDRQTY_AFT.ToString(),                               // 현   출고량
                                                                    this.CBH01_CHACTHJ.GetValue().ToString().Trim(),
                                                                    this.CBH01_CHHWAMUL.GetValue().ToString().Trim(),
                                                                    Get_Date(this.DTP01_CHIPHANG.GetValue().ToString().Trim()),
                                                                    this.CBH01_CHBONSUN.GetValue().ToString().Trim(),
                                                                    this.CBH01_CHHWAJU.GetValue().ToString().Trim(),
                                                                    this.TXT01_CHBLNO.GetValue().ToString().Trim(),
                                                                    Get_Numeric(this.TXT01_CHMSNSEQ.GetValue().ToString().Trim()),
                                                                    Get_Numeric(this.TXT01_CHHSNSEQ.GetValue().ToString().Trim()),
                                                                    Get_Date(this.DTP01_CHCUSTIL.GetValue().ToString().Trim()),
                                                                    Get_Numeric(this.TXT01_CHCHASU.GetValue().ToString().Trim()),
                                                                    this.TXT01_CHJGHWAJU.GetValue().ToString(),
                                                                    this.CBH01_CHYSHWAJU.GetValue().ToString(),
                                                                    this.CBH01_CHYDHWAJU.GetValue().ToString(),
                                                                    Get_Date(this.DTP01_CHYSDATE.GetValue().ToString().Trim()),
                                                                    this.TXT01_CHYDSEQ.GetValue().ToString(),
                                                                    this.TXT01_CHYSSEQ.GetValue().ToString(),
                                                                    Get_Numeric(this.TXT01_CHJUNG.GetValue().ToString().Trim())
                                                                    );
                    }

                    if (sDRUT_GUBUN == "UPT")
                    {
                        // 탱크별 DRUM 재고
                        this.DbConnector.Attach("TY_P_UT_6CRF3235", "0",                                                   // 이전 출고량
                                                                    sCHDRQTY_AFT.ToString(),                               // 현   출고량
                                                                    "0",                                                   // 이전 출고량
                                                                    sCHDRQTY_AFT.ToString(),                               // 현   출고량
                                                                    this.CBH01_CHACTHJ.GetValue().ToString().Trim(),
                                                                    this.CBH01_CHHWAMUL.GetValue().ToString().Trim(),
                                                                    Get_Date(this.DTP01_CHIPHANG.GetValue().ToString().Trim()),
                                                                    this.CBH01_CHBONSUN.GetValue().ToString().Trim(),
                                                                    this.CBH01_CHHWAJU.GetValue().ToString().Trim(),
                                                                    this.TXT01_CHCHTANK.GetValue().ToString().Trim(),
                                                                    Get_Numeric(this.TXT01_CHJUNG.GetValue().ToString().Trim())
                                                                    );
                    }
                }

                #endregion

                this.DbConnector.ExecuteTranQueryList();

                #endregion
            }
            else // 수정
            {
                #region Description : 수정

                sCHCHSTR = Set_Fill2(this.TXT01_CHCHSTR1.GetValue().ToString()) + Set_Fill2(this.TXT01_CHCHSTR2.GetValue().ToString());
                sCHCHEND = Set_Fill2(this.TXT01_CHCHEND1.GetValue().ToString()) + Set_Fill2(this.TXT01_CHCHEND2.GetValue().ToString());
                sCHOVSTR = Set_Fill2(this.TXT01_CHOVSTR1.GetValue().ToString()) + Set_Fill2(this.TXT01_CHOVSTR2.GetValue().ToString());
                sCHOVEND = Set_Fill2(this.TXT01_CHOVEND1.GetValue().ToString()) + Set_Fill2(this.TXT01_CHOVEND2.GetValue().ToString());

                sCHJISINUM = Get_Date(this.TXT01_CHJISINUM1.GetValue().ToString()) + Set_Fill3(TXT01_CHJISINUM2.GetValue().ToString());

                this.DbConnector.CommandClear();

                #region Description : 출고 LOG 등록

                this.DbConnector.Attach("TY_P_UT_6CSAQ240", Get_Date(this.DTP01_CHCHULIL.GetValue().ToString()),   // 출고일자
                                                            this.TXT01_CHTKNO.GetValue().ToString()                // 순번
                                                            );

                #endregion

                #region Description : 출고관리 업데이트

                string sCHHIGB = string.Empty;

                if (fsCHHIGB.ToString() == "1" || fsCHHIGB.ToString() == "2")
                {
                    sCHHIGB = "2";
                }
                else
                {
                    sCHHIGB = "C";
                }

                this.DbConnector.Attach("TY_P_UT_83CGY691", fsCHYNGUBUN.ToString(),                                // 양수구분
                                                            fsCHYNCHQTY_AFT.ToString().ToString(),                 // 양수출고량
                                                            this.CBH01_CHCHHJ.GetValue().ToString(),               // 출고화주
                                                            Get_Date(this.DTP01_CHENDIL.GetValue().ToString()),    // 출고종료일
                                                            Set_TankNo(this.TXT01_CHCHTANK.GetValue().ToString()), // 출고탱크
                                                            Set_TankNo(this.TXT01_CHIPTANK.GetValue().ToString()), // 입고탱크
                                                            this.TXT01_CHCONTNO.GetValue().ToString(),             // 계약번호
                                                            this.CBH01_CHCHULGB.GetValue().ToString(),             // 출하구분
                                                            this.CBO01_CHWKTYPE.GetValue().ToString(),             // 작업방법
                                                            sCHCHSTR.ToString(),
                                                            sCHCHEND.ToString(),
                                                            this.CBH01_CHCHJANG.GetValue().ToString(),
                                                            this.TXT01_CHMTQTY.GetValue().ToString(),
                                                            this.TXT01_CHKLQTY.GetValue().ToString(),
                                                            this.CBO01_CHDANYI.GetValue().ToString(),
                                                            this.TXT01_CHQTY.GetValue().ToString(),
                                                            fsCHBIJUNG.ToString(),
                                                            fsCHVCF.ToString(),
                                                            sCHOVSTR.ToString(),
                                                            sCHOVEND.ToString(),
                                                            this.TXT01_CHOVQTY.GetValue().ToString(),
                                                            this.TXT01_CHOVAM.GetValue().ToString(),
                                                            this.CBH01_CHSOSOK.GetValue().ToString(),
                                                            this.CBH01_CHCHHASAB.GetValue().ToString(),
                                                            this.CBH01_CHCHSAB.GetValue().ToString(),
                                                            Get_Date(this.DTP01_CHSUCHIP.GetValue().ToString()),
                                                            this.CBH01_CHSUCHVS.GetValue().ToString(),
                                                            fsCHHWAPE.ToString(),
                                                            this.TXT01_CHJUNG.GetValue().ToString(),
                                                            sCHJISINUM.ToString(),
                                                            this.CBH01_CHJISAB.GetValue().ToString(),
                                                            this.TXT01_CHEMPTY.GetValue().ToString(),
                                                            this.TXT01_CHTOTAL.GetValue().ToString(),
                                                            this.TXT01_CHCARNO.GetValue().ToString(),
                                                            this.CBH01_CHOUTCHSAB.GetValue().ToString(),
                                                            this.TXT01_CHCONTNUM.GetValue().ToString(),
                                                            this.TXT01_CHSILNUM.GetValue().ToString(),
                                                            this.CBH01_CHDNST.GetValue().ToString(),
                                                            sCHHIGB.ToString(),
                                                            TYUserInfo.EmpNo.ToString().Trim().ToUpper(),          // 작성사번
                                                            Get_Date(this.DTP01_CHIPHANG.GetValue().ToString()),   // 입항일자
                                                            this.CBH01_CHBONSUN.GetValue().ToString(),             // 본선
                                                            this.CBH01_CHHWAJU.GetValue().ToString(),              // 화주
                                                            this.CBH01_CHHWAMUL.GetValue().ToString(),             // 화물
                                                            this.TXT01_CHBLNO.GetValue().ToString(),               // BL번호
                                                            this.TXT01_CHMSNSEQ.GetValue().ToString(),             // MSN번호
                                                            this.TXT01_CHHSNSEQ.GetValue().ToString(),             // HSN번호
                                                            Get_Date(this.DTP01_CHCUSTIL.GetValue().ToString()),   // 통관일자
                                                            this.TXT01_CHCHASU.GetValue().ToString(),              // 통관차수
                                                            this.CBH01_CHACTHJ.GetValue().ToString(),              // 통관화주
                                                            this.TXT01_CHJGHWAJU.GetValue().ToString(),            // 재고화주
                                                            this.CBH01_CHYSHWAJU.GetValue().ToString(),            // 양수화주
                                                            this.CBH01_CHYDHWAJU.GetValue().ToString(),            // 양도화주
                                                            Get_Date(this.DTP01_CHYSDATE.GetValue().ToString()),   // 양수일자
                                                            this.TXT01_CHYDSEQ.GetValue().ToString(),              // 양도차수
                                                            this.TXT01_CHYSSEQ.GetValue().ToString(),              // 양수순번
                                                            Get_Date(this.DTP01_CHCHULIL.GetValue().ToString()),   // 출고일자
                                                            this.TXT01_CHTKNO.GetValue().ToString()                // 순번
                                                            );

                #endregion

                #region Description : 출고누계

                if (sCHNU_GUBUN == "INS")
                {
                    // 출고누계 등록
                    this.DbConnector.Attach("TY_P_UT_6CQE6214", Get_Date(this.DTP01_CHCHULIL.GetValue().ToString().Trim()).Substring(0, 4).ToString(),
                                                                Get_Date(this.DTP01_CHCHULIL.GetValue().ToString().Trim()).Substring(4, 2).ToString(),
                                                                this.CBH01_CHHWAJU.GetValue().ToString().Trim(),
                                                                this.CBH01_CHHWAMUL.GetValue().ToString().Trim(),
                                                                this.CBH01_CHACTHJ.GetValue().ToString().Trim(),
                                                                this.CBH01_CHCHHJ.GetValue().ToString().Trim(),
                                                                Get_Numeric(this.TXT01_CHMTQTY.GetValue().ToString())
                                                                );
                }
                else
                {
                    // 출고누계 업데이트
                    this.DbConnector.Attach("TY_P_UT_6CQE1216", Get_Numeric(fsCHMTQTY.ToString()),                     // 이전 출고량
                                                                Get_Numeric(this.TXT01_CHMTQTY.GetValue().ToString()), // 현 출고량
                                                                Get_Date(this.DTP01_CHCHULIL.GetValue().ToString().Trim()).Substring(0, 4).ToString(),
                                                                Get_Date(this.DTP01_CHCHULIL.GetValue().ToString().Trim()).Substring(4, 2).ToString(),
                                                                this.CBH01_CHHWAJU.GetValue().ToString().Trim(),
                                                                this.CBH01_CHHWAMUL.GetValue().ToString().Trim(),
                                                                this.CBH01_CHACTHJ.GetValue().ToString().Trim(),
                                                                this.CBH01_CHCHHJ.GetValue().ToString().Trim()
                                                                );
                }

                #endregion

                #region Description : 매출입고 할증

                this.DbConnector.Attach("TY_P_UT_6CQEH217", Get_Numeric(fsCHMTQTY.ToString()),                     // 이전 출고량
                                                            Get_Numeric(this.TXT01_CHMTQTY.GetValue().ToString()), // 현 출고량
                                                            Get_Date(this.DTP01_CHIPHANG.GetValue().ToString()),   // 입항일자
                                                            this.CBH01_CHBONSUN.GetValue().ToString(),             // 본선
                                                            this.CBH01_CHHWAJU.GetValue().ToString(),              // 화주
                                                            this.CBH01_CHHWAMUL.GetValue().ToString(),             // 화물
                                                            this.TXT01_CHIPTANK.GetValue().ToString().Trim()       // 입고탱크
                                                            );

                #endregion

                #region Description : SURVEY 파일

                if (this.CBO01_CHDANYI.GetValue().ToString() != "2")
                {
                    this.DbConnector.Attach("TY_P_UT_6CQEK218", Get_Numeric(fsCHMTQTY.ToString()),                     // 이전 출고량
                                                                Get_Numeric(this.TXT01_CHMTQTY.GetValue().ToString()), // 현 출고량
                                                                Get_Date(this.DTP01_CHIPHANG.GetValue().ToString()),   // 입항일자
                                                                this.CBH01_CHBONSUN.GetValue().ToString(),             // 본선
                                                                this.CBH01_CHHWAJU.GetValue().ToString(),              // 화주
                                                                this.CBH01_CHHWAMUL.GetValue().ToString(),             // 화물
                                                                this.TXT01_CHCHTANK.GetValue().ToString().Trim()       // 출고탱크
                                                                );
                }

                #endregion

                #region Description : B/L별 입고파일

                this.DbConnector.Attach("TY_P_UT_6CQES219", Get_Numeric(fsCHMTQTY.ToString()),                     // 이전 출고량
                                                            Get_Numeric(this.TXT01_CHMTQTY.GetValue().ToString()), // 현 출고량
                                                            Get_Date(this.DTP01_CHIPHANG.GetValue().ToString()),   // 입항일자
                                                            this.CBH01_CHBONSUN.GetValue().ToString(),             // 본선
                                                            this.CBH01_CHHWAJU.GetValue().ToString(),              // 화주
                                                            this.CBH01_CHHWAMUL.GetValue().ToString(),             // 화물
                                                            this.TXT01_CHBLNO.GetValue().ToString(),               // BL번호
                                                            this.TXT01_CHMSNSEQ.GetValue().ToString(),             // MSN번호
                                                            this.TXT01_CHHSNSEQ.GetValue().ToString()              // HSN번호
                                                            );

                #endregion

                #region Description : 통관파일

                this.DbConnector.Attach("TY_P_UT_6CQEX220", Get_Numeric(fsCHMTQTY.ToString()),                     // 이전 출고량
                                                            Get_Numeric(this.TXT01_CHMTQTY.GetValue().ToString()), // 현 출고량
                                                            Get_Date(this.DTP01_CHIPHANG.GetValue().ToString()),   // 입항일자
                                                            this.CBH01_CHBONSUN.GetValue().ToString(),             // 본선
                                                            this.CBH01_CHHWAJU.GetValue().ToString(),              // 화주
                                                            this.CBH01_CHHWAMUL.GetValue().ToString(),             // 화물
                                                            this.TXT01_CHBLNO.GetValue().ToString(),               // BL번호
                                                            this.TXT01_CHMSNSEQ.GetValue().ToString(),             // MSN번호
                                                            this.TXT01_CHHSNSEQ.GetValue().ToString(),             // HSN번호
                                                            Get_Date(this.DTP01_CHCUSTIL.GetValue().ToString()),   // 통관일자
                                                            this.TXT01_CHCHASU.GetValue().ToString()               // 통관차수
                                                            );

                #endregion

                #region Description : 통관화주별 재고 파일

                string sCHMTQTY = string.Empty;

                sCHMTQTY = "0";
                fsCHYNCHQTY_AFT = "0";

                if (this.CBH01_CHYSHWAJU.GetValue().ToString() != "" && this.CBH01_CHYDHWAJU.GetValue().ToString() != "" &&
                    Get_Date(this.DTP01_CHYSDATE.GetValue().ToString().Trim()) != "0" && Get_Numeric(this.TXT01_CHYDSEQ.GetValue().ToString()) != "0" &&
                    Get_Numeric(this.TXT01_CHYSSEQ.GetValue().ToString()) != "0")
                {
                    fsCHYNGUBUN = "R";

                    fsCHMTQTY = "0";

                    fsCHYNCHQTY_AFT = this.TXT01_CHMTQTY.GetValue().ToString();
                }
                else
                {
                    sCHMTQTY = Get_Numeric(this.TXT01_CHMTQTY.GetValue().ToString());
                }

                this.DbConnector.Attach("TY_P_UT_6CQF8221", fsCHYNCHQTY_AGO,                                       // 이전 양수출고량
                                                            fsCHYNCHQTY_AFT,                                       // 이후 양수출고량
                                                            fsCHMTQTY,                                             // 이전 출고량
                                                            sCHMTQTY,                                              // 현   출고량
                                                            fsCHYNCHQTY_AGO,                                       // 이전 양수출고량
                                                            fsCHMTQTY,                                             // 이전 출고량
                                                            fsCHYNCHQTY_AFT,                                       // 이후 양수출고량
                                                            sCHMTQTY,                                              // 현   출고량
                                                            this.CBH01_CHACTHJ.GetValue().ToString(),              // 통관화주
                                                            this.TXT01_CHJGHWAJU.GetValue().ToString(),            // 재고화주
                                                            Get_Date(this.DTP01_CHIPHANG.GetValue().ToString()),   // 입항일자
                                                            this.CBH01_CHBONSUN.GetValue().ToString(),             // 본선
                                                            this.CBH01_CHHWAJU.GetValue().ToString(),              // 화주
                                                            this.CBH01_CHHWAMUL.GetValue().ToString(),             // 화물
                                                            this.TXT01_CHBLNO.GetValue().ToString(),               // BL번호
                                                            this.TXT01_CHMSNSEQ.GetValue().ToString(),             // MSN번호
                                                            this.TXT01_CHHSNSEQ.GetValue().ToString(),             // HSN번호
                                                            Get_Date(this.DTP01_CHCUSTIL.GetValue().ToString()),   // 통관일자
                                                            this.TXT01_CHCHASU.GetValue().ToString(),              // 통관차수
                                                            this.CBH01_CHYSHWAJU.GetValue().ToString(),            // 양수화주
                                                            this.CBH01_CHYDHWAJU.GetValue().ToString(),            // 양도화주
                                                            Get_Date(this.DTP01_CHYSDATE.GetValue().ToString()),   // 양수일자
                                                            this.TXT01_CHYDSEQ.GetValue().ToString(),              // 양도차수
                                                            this.TXT01_CHYSSEQ.GetValue().ToString()               // 양수순번
                                                            );

                #endregion

                #region Description : 양수도 파일

                if (this.CBH01_CHYSHWAJU.GetValue().ToString() != "" && this.CBH01_CHYDHWAJU.GetValue().ToString() != "" &&
                   Get_Date(this.DTP01_CHYSDATE.GetValue().ToString().Trim()) != "0" && Get_Numeric(this.TXT01_CHYDSEQ.GetValue().ToString()) != "0" &&
                   Get_Numeric(this.TXT01_CHYSSEQ.GetValue().ToString()) != "0")
                {
                    this.DbConnector.Attach("TY_P_UT_6CQFZ222", fsCHYNCHQTY_AGO,                                       // 이전 양수출고량
                                                                fsCHYNCHQTY_AFT.ToString(),                            // 이후 양수출고량
                                                                fsCHYNCHQTY_AGO,                                       // 이전 양수출고량
                                                                fsCHYNCHQTY_AFT.ToString(),                            // 이후 양수출고량
                                                                Get_Date(this.DTP01_CHIPHANG.GetValue().ToString()),   // 입항일자
                                                                this.CBH01_CHBONSUN.GetValue().ToString(),             // 본선
                                                                this.CBH01_CHHWAJU.GetValue().ToString(),              // 화주
                                                                this.CBH01_CHHWAMUL.GetValue().ToString(),             // 화물
                                                                this.TXT01_CHBLNO.GetValue().ToString(),               // BL번호
                                                                this.TXT01_CHMSNSEQ.GetValue().ToString(),             // MSN번호
                                                                this.TXT01_CHHSNSEQ.GetValue().ToString(),             // HSN번호
                                                                Get_Date(this.DTP01_CHCUSTIL.GetValue().ToString()),   // 통관일자
                                                                this.TXT01_CHCHASU.GetValue().ToString(),              // 통관차수
                                                                this.CBH01_CHACTHJ.GetValue().ToString(),              // 통관화주
                                                                this.CBH01_CHYDHWAJU.GetValue().ToString(),            // 양도화주
                                                                this.CBH01_CHYSHWAJU.GetValue().ToString(),            // 양수화주
                                                                Get_Date(this.DTP01_CHYSDATE.GetValue().ToString()),   // 양수일자
                                                                this.TXT01_CHYDSEQ.GetValue().ToString(),              // 양도차수
                                                                this.TXT01_CHYSSEQ.GetValue().ToString()               // 양수순번
                                                                );
                }

                #endregion

                #region Description : 일자별 DRUM 출고파일

                if (this.CBO01_CHDANYI.GetValue().ToString() == "2")
                {
                    sCHDRQTY_AFT = this.TXT01_CHQTY.GetValue().ToString();

                    // 일자별 DRUM 출고파일 업데이트
                    this.DbConnector.Attach("TY_P_UT_6CQCY207", fsCHDRQTY.ToString(),                                  // 이전 드럼개수
                                                                sCHDRQTY_AFT.ToString(),                               // 이후 드럼개수
                                                                fsCHMTQTY.ToString(),                                  // 이전 MT량
                                                                this.TXT01_CHMTQTY.GetValue().ToString(),              // 이후 MT량
                                                                fsCHKLQTY.ToString(),                                  // 이전 KL량
                                                                this.TXT01_CHKLQTY.GetValue().ToString(),              // 이후 KL량
                                                                Get_Date(this.DTP01_CHCHULIL.GetValue().ToString()),   // 출고일자
                                                                this.CBH01_CHACTHJ.GetValue().ToString(),              // 통관화주
                                                                Get_Date(this.DTP01_CHIPHANG.GetValue().ToString()),   // 입항일자
                                                                this.CBH01_CHBONSUN.GetValue().ToString(),             // 본선
                                                                this.CBH01_CHHWAJU.GetValue().ToString(),              // 화주
                                                                this.CBH01_CHHWAMUL.GetValue().ToString(),             // 화물
                                                                this.TXT01_CHBLNO.GetValue().ToString(),               // BL번호
                                                                this.TXT01_CHMSNSEQ.GetValue().ToString(),             // MSN번호
                                                                this.TXT01_CHHSNSEQ.GetValue().ToString(),             // HSN번호
                                                                Get_Date(this.DTP01_CHCUSTIL.GetValue().ToString()),   // 통관일자
                                                                this.TXT01_CHCHASU.GetValue().ToString(),              // 통관차수
                                                                this.TXT01_CHJGHWAJU.GetValue().ToString(),            // 재고화주
                                                                this.CBH01_CHYSHWAJU.GetValue().ToString(),            // 양수화주
                                                                this.CBH01_CHYDHWAJU.GetValue().ToString(),            // 양도화주
                                                                Get_Date(this.DTP01_CHYSDATE.GetValue().ToString()),   // 양수일자
                                                                this.TXT01_CHYDSEQ.GetValue().ToString(),              // 양도차수
                                                                this.TXT01_CHYSSEQ.GetValue().ToString(),              // 양수순번
                                                                this.TXT01_CHJUNG.GetValue().ToString()                // 중량
                                                        );
                }

                #endregion

                #region Description : 출고지시 및 DRUM 지시

                // 케미컬 출고일 경우
                if (this.CBO01_CHDANYI.GetValue().ToString() != "2")
                {
                    if (this.TXT01_CHJISINUM1.GetValue().ToString() != "" &&
                        this.TXT01_CHJISINUM2.GetValue().ToString() != "")
                    {
                        // 출고 지시
                        this.DbConnector.Attach("TY_P_UT_6CR9Y228", fsCHMTQTY.ToString(),                                  // 이전 출고량
                                                                    Get_Numeric(this.TXT01_CHMTQTY.GetValue().ToString()), // 현   출고량
                                                                    fsCHMTQTY.ToString(),                                  // 이전 출고량
                                                                    Get_Numeric(this.TXT01_CHMTQTY.GetValue().ToString()), // 현   출고량
                                                                    this.TXT01_CHJISINUM1.GetValue().ToString(),           // 지시일자
                                                                    this.TXT01_CHJISINUM2.GetValue().ToString()            // 지시순번
                                                                    );
                    }
                }
                else
                {
                    if (sJIDR_GUBUN == "UPT")
                    {
                        // DRUM 지시
                        this.DbConnector.Attach("TY_P_UT_6CRED233", fsCHDRQTY.ToString(),                                  // 이전 DRUM 개수
                                                                    sCHDRQTY_AFT.ToString(),                               // 현   DRUM 개수
                                                                    fsCHDRQTY.ToString(),                                  // 이전 DRUM 개수
                                                                    sCHDRQTY_AFT.ToString(),                               // 현   DRUM 개수
                                                                    fsCHDRQTY.ToString(),                                  // 이전 DRUM 개수
                                                                    sCHDRQTY_AFT.ToString(),                               // 현   DRUM 개수
                                                                    this.TXT01_CHJISINUM1.GetValue().ToString(),           // 지시일자
                                                                    this.TXT01_CHJISINUM2.GetValue().ToString(),           // 지시순번
                                                                    Get_Date(this.DTP01_CHIPHANG.GetValue().ToString().Trim()),
                                                                    this.CBH01_CHBONSUN.GetValue().ToString().Trim().ToUpper(),
                                                                    this.CBH01_CHHWAJU.GetValue().ToString().Trim().ToUpper(),
                                                                    this.CBH01_CHHWAMUL.GetValue().ToString().Trim().ToUpper(),
                                                                    this.TXT01_CHBLNO.GetValue().ToString().Trim(),
                                                                    Get_Numeric(this.TXT01_CHMSNSEQ.GetValue().ToString().Trim()),
                                                                    Get_Numeric(this.TXT01_CHHSNSEQ.GetValue().ToString().Trim()),
                                                                    this.CBH01_CHACTHJ.GetValue().ToString().Trim().ToUpper(),
                                                                    Get_Date(this.DTP01_CHCUSTIL.GetValue().ToString().Trim()),
                                                                    Get_Numeric(this.TXT01_CHCHASU.GetValue().ToString().Trim()),
                                                                    this.TXT01_CHJGHWAJU.GetValue().ToString(),
                                                                    this.CBH01_CHYSHWAJU.GetValue().ToString(),
                                                                    this.CBH01_CHYDHWAJU.GetValue().ToString(),
                                                                    Get_Date(this.DTP01_CHYSDATE.GetValue().ToString().Trim()),
                                                                    this.TXT01_CHYDSEQ.GetValue().ToString(),
                                                                    this.TXT01_CHYSSEQ.GetValue().ToString()
                                                                    );
                    }
                }

                #endregion

                #region Description : 드럼 출고

                if (this.CBO01_CHDANYI.GetValue().ToString() == "2")
                {
                    if (sDRUJ_GUBUN == "UPT")
                    {
                        // DRUM 재고
                        this.DbConnector.Attach("TY_P_UT_6CREZ234", fsCHDRQTY.ToString(),                                  // 이전 DRUM 개수
                                                                    sCHDRQTY_AFT.ToString(),                               // 현   DRUM 개수
                                                                    fsCHDRQTY.ToString(),                                  // 이전 DRUM 개수
                                                                    sCHDRQTY_AFT.ToString(),                               // 현   DRUM 개수
                                                                    this.CBH01_CHACTHJ.GetValue().ToString().Trim(),
                                                                    this.CBH01_CHHWAMUL.GetValue().ToString().Trim(),
                                                                    Get_Date(this.DTP01_CHIPHANG.GetValue().ToString().Trim()),
                                                                    this.CBH01_CHBONSUN.GetValue().ToString().Trim(),
                                                                    this.CBH01_CHHWAJU.GetValue().ToString().Trim(),
                                                                    this.TXT01_CHBLNO.GetValue().ToString().Trim(),
                                                                    Get_Numeric(this.TXT01_CHMSNSEQ.GetValue().ToString().Trim()),
                                                                    Get_Numeric(this.TXT01_CHHSNSEQ.GetValue().ToString().Trim()),
                                                                    Get_Date(this.DTP01_CHCUSTIL.GetValue().ToString().Trim()),
                                                                    Get_Numeric(this.TXT01_CHCHASU.GetValue().ToString().Trim()),
                                                                    this.TXT01_CHJGHWAJU.GetValue().ToString(),
                                                                    this.CBH01_CHYSHWAJU.GetValue().ToString(),
                                                                    this.CBH01_CHYDHWAJU.GetValue().ToString(),
                                                                    Get_Date(this.DTP01_CHYSDATE.GetValue().ToString().Trim()),
                                                                    this.TXT01_CHYDSEQ.GetValue().ToString(),
                                                                    this.TXT01_CHYSSEQ.GetValue().ToString(),
                                                                    Get_Numeric(this.TXT01_CHJUNG.GetValue().ToString().Trim())
                                                                    );
                    }

                    if (sDRUT_GUBUN == "UPT")
                    {
                        // 탱크별 DRUM 재고
                        this.DbConnector.Attach("TY_P_UT_6CRF3235", fsCHDRQTY.ToString(),                                  // 이전 DRUM 개수
                                                                    sCHDRQTY_AFT.ToString(),                               // 현   DRUM 개수
                                                                    fsCHDRQTY.ToString(),                                  // 이전 DRUM 개수
                                                                    sCHDRQTY_AFT.ToString(),                               // 현   DRUM 개수
                                                                    this.CBH01_CHACTHJ.GetValue().ToString().Trim(),
                                                                    this.CBH01_CHHWAMUL.GetValue().ToString().Trim(),
                                                                    Get_Date(this.DTP01_CHIPHANG.GetValue().ToString().Trim()),
                                                                    this.CBH01_CHBONSUN.GetValue().ToString().Trim(),
                                                                    this.CBH01_CHHWAJU.GetValue().ToString().Trim(),
                                                                    this.TXT01_CHCHTANK.GetValue().ToString().Trim(),
                                                                    Get_Numeric(this.TXT01_CHJUNG.GetValue().ToString().Trim())
                                                                    );
                    }
                }

                #endregion

                this.DbConnector.ExecuteTranQueryList();

                #endregion
            }

            UP_BUTTON_Visible("");

            this.BTN61_INQ_Click(null, null);

            this.ShowMessage("TY_M_GB_23NAD873");
        }
        #endregion

        #region Description : 삭제 버튼
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            string sCHCHSTR = string.Empty;
            string sCHCHEND = string.Empty;
            string sCHOVSTR = string.Empty;
            string sCHOVEND = string.Empty;
            string sCHJISINUM = string.Empty;


            string sCHDRQTY_AFT = string.Empty;
            string sCHMTQTY_AFT = string.Empty;
            string sCHKLQTY_AFT = string.Empty;

            string sDRUC_GUBUN = string.Empty;
            string sCHNU_GUBUN = string.Empty;
            string sJIDR_GUBUN = string.Empty;
            string sDRUJ_GUBUN = string.Empty;
            string sDRUT_GUBUN = string.Empty;

            int iDRUC_CNT = 0;

            string sJCSEQ = string.Empty;

            DataTable dt = new DataTable();

            sDRUC_GUBUN = "";

            // 일자별 DRUM 출고 파일 체크
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_6CLHF155",
                Get_Date(this.DTP01_CHCHULIL.GetValue().ToString().Trim()),
                this.CBH01_CHACTHJ.GetValue().ToString().Trim(),
                this.CBH01_CHHWAMUL.GetValue().ToString().Trim(),
                Get_Date(this.DTP01_CHIPHANG.GetValue().ToString().Trim()),
                this.CBH01_CHBONSUN.GetValue().ToString().Trim(),
                this.CBH01_CHHWAJU.GetValue().ToString().Trim(),
                this.TXT01_CHBLNO.GetValue().ToString().Trim(),
                Get_Numeric(this.TXT01_CHMSNSEQ.GetValue().ToString().Trim()),
                Get_Numeric(this.TXT01_CHHSNSEQ.GetValue().ToString().Trim()),
                Get_Date(this.DTP01_CHCUSTIL.GetValue().ToString().Trim()),
                Get_Numeric(this.TXT01_CHCHASU.GetValue().ToString().Trim()),
                this.TXT01_CHJGHWAJU.GetValue().ToString(),
                this.CBH01_CHYSHWAJU.GetValue().ToString(),
                this.CBH01_CHYDHWAJU.GetValue().ToString(),
                Get_Date(this.DTP01_CHYSDATE.GetValue().ToString().Trim()),
                this.TXT01_CHYDSEQ.GetValue().ToString(),
                this.TXT01_CHYSSEQ.GetValue().ToString(),
                Get_Numeric(this.TXT01_CHJUNG.GetValue().ToString().Trim())
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count <= 0)
            {
                sDRUC_GUBUN = "INS";
            }
            else
            {
                sDRUC_GUBUN = "UPT";

                iDRUC_CNT = dt.Rows.Count;
            }


            // 출고 누계 체크
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_6CNGT202",
                Get_Date(this.DTP01_CHCHULIL.GetValue().ToString().Trim()).Substring(0, 4).ToString(),
                Get_Date(this.DTP01_CHCHULIL.GetValue().ToString().Trim()).Substring(4, 2).ToString(),
                this.CBH01_CHHWAJU.GetValue().ToString().Trim(),
                this.CBH01_CHHWAMUL.GetValue().ToString().Trim(),
                this.CBH01_CHACTHJ.GetValue().ToString().Trim(),
                this.CBH01_CHCHHJ.GetValue().ToString().Trim()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count <= 0)
            {
                sCHNU_GUBUN = "INS";
            }
            else
            {
                sCHNU_GUBUN = "UPT";
            }

            // DRUM 지시 체크
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_6CNHM203",
                Get_Date(this.DTP01_CHIPHANG.GetValue().ToString().Trim()),
                this.CBH01_CHBONSUN.GetValue().ToString().Trim().ToUpper(),
                this.CBH01_CHHWAJU.GetValue().ToString().Trim().ToUpper(),
                this.CBH01_CHHWAMUL.GetValue().ToString().Trim().ToUpper(),
                this.TXT01_CHBLNO.GetValue().ToString().Trim(),
                Get_Numeric(this.TXT01_CHMSNSEQ.GetValue().ToString().Trim()),
                Get_Numeric(this.TXT01_CHHSNSEQ.GetValue().ToString().Trim()),
                this.CBH01_CHACTHJ.GetValue().ToString().Trim().ToUpper(),
                Get_Date(this.DTP01_CHCUSTIL.GetValue().ToString().Trim()),
                Get_Numeric(this.TXT01_CHCHASU.GetValue().ToString().Trim()),
                this.TXT01_CHJGHWAJU.GetValue().ToString(),
                this.CBH01_CHYSHWAJU.GetValue().ToString(),
                this.CBH01_CHYDHWAJU.GetValue().ToString(),
                Get_Date(this.DTP01_CHYSDATE.GetValue().ToString().Trim()),
                this.TXT01_CHYDSEQ.GetValue().ToString(),
                this.TXT01_CHYSSEQ.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                sJIDR_GUBUN = "UPT";
            }

            // DRUM 재고 체크
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_6CLGO147",
                this.CBH01_CHACTHJ.GetValue().ToString().Trim(),
                this.CBH01_CHHWAMUL.GetValue().ToString().Trim(),
                Get_Date(this.DTP01_CHIPHANG.GetValue().ToString().Trim()),
                this.CBH01_CHBONSUN.GetValue().ToString().Trim(),
                this.CBH01_CHHWAJU.GetValue().ToString().Trim(),
                this.TXT01_CHBLNO.GetValue().ToString().Trim(),
                Get_Numeric(this.TXT01_CHMSNSEQ.GetValue().ToString().Trim()),
                Get_Numeric(this.TXT01_CHHSNSEQ.GetValue().ToString().Trim()),
                Get_Date(this.DTP01_CHCUSTIL.GetValue().ToString().Trim()),
                Get_Numeric(this.TXT01_CHCHASU.GetValue().ToString().Trim()),
                this.TXT01_CHJGHWAJU.GetValue().ToString(),
                this.CBH01_CHYSHWAJU.GetValue().ToString(),
                this.CBH01_CHYDHWAJU.GetValue().ToString(),
                Get_Date(this.DTP01_CHYSDATE.GetValue().ToString().Trim()),
                this.TXT01_CHYDSEQ.GetValue().ToString(),
                this.TXT01_CHYSSEQ.GetValue().ToString(),
                Get_Numeric(this.TXT01_CHJUNG.GetValue().ToString().Trim())
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                sDRUJ_GUBUN = "UPT";
            }

            // 출고 - 탱크별 DRUM 재고 체크
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_6CLH8151",
                this.CBH01_CHACTHJ.GetValue().ToString().Trim(),
                this.CBH01_CHHWAMUL.GetValue().ToString().Trim(),
                Get_Date(this.DTP01_CHIPHANG.GetValue().ToString().Trim()),
                this.CBH01_CHBONSUN.GetValue().ToString().Trim(),
                this.CBH01_CHHWAJU.GetValue().ToString().Trim(),
                this.TXT01_CHCHTANK.GetValue().ToString().Trim(),
                Get_Numeric(this.TXT01_CHJUNG.GetValue().ToString().Trim())
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                sDRUT_GUBUN = "UPT";
            }



            this.DbConnector.CommandClear();

            #region Description : 출고 LOG 등록

            this.DbConnector.Attach("TY_P_UT_6CSAQ240", Get_Date(this.DTP01_CHCHULIL.GetValue().ToString()),   // 출고일자
                                                        this.TXT01_CHTKNO.GetValue().ToString()                // 순번
                                                        );

            #endregion

            #region Description : 출고관리 삭제

            this.DbConnector.Attach("TY_P_UT_6CQDS210", Get_Date(this.DTP01_CHIPHANG.GetValue().ToString()),   // 입항일자
                                                       this.CBH01_CHBONSUN.GetValue().ToString(),             // 본선
                                                       this.CBH01_CHHWAJU.GetValue().ToString(),              // 화주
                                                       this.CBH01_CHHWAMUL.GetValue().ToString(),             // 화물
                                                       this.TXT01_CHBLNO.GetValue().ToString(),               // BL번호
                                                       this.TXT01_CHMSNSEQ.GetValue().ToString(),             // MSN번호
                                                       this.TXT01_CHHSNSEQ.GetValue().ToString(),             // HSN번호
                                                       Get_Date(this.DTP01_CHCUSTIL.GetValue().ToString()),   // 통관일자
                                                       this.TXT01_CHCHASU.GetValue().ToString(),              // 통관차수
                                                       this.CBH01_CHACTHJ.GetValue().ToString(),              // 통관화주
                                                       this.TXT01_CHJGHWAJU.GetValue().ToString(),            // 재고화주
                                                       this.CBH01_CHYSHWAJU.GetValue().ToString(),            // 양수화주
                                                       this.CBH01_CHYDHWAJU.GetValue().ToString(),            // 양도화주
                                                       Get_Date(this.DTP01_CHYSDATE.GetValue().ToString()),   // 양수일자
                                                       this.TXT01_CHYDSEQ.GetValue().ToString(),              // 양도차수
                                                       this.TXT01_CHYSSEQ.GetValue().ToString(),              // 양수순번
                                                       Get_Date(this.DTP01_CHCHULIL.GetValue().ToString()),   // 출고일자
                                                       this.TXT01_CHTKNO.GetValue().ToString()                // 순번
                                                       );

            #endregion

            #region Description : 출고누계

            // 출고누계 업데이트
            this.DbConnector.Attach("TY_P_UT_6CQE1216", Get_Numeric(fsCHMTQTY.ToString()),                     // 이전 출고량
                                                        "0",                                                   // 현 출고량
                                                        Get_Date(this.DTP01_CHCHULIL.GetValue().ToString().Trim()).Substring(0, 4).ToString(),
                                                        Get_Date(this.DTP01_CHCHULIL.GetValue().ToString().Trim()).Substring(4, 2).ToString(),
                                                        this.CBH01_CHHWAJU.GetValue().ToString().Trim(),
                                                        this.CBH01_CHHWAMUL.GetValue().ToString().Trim(),
                                                        this.CBH01_CHACTHJ.GetValue().ToString().Trim(),
                                                        this.CBH01_CHCHHJ.GetValue().ToString().Trim()
                                                        );

            #endregion

            #region Description : 매출입고 할증

            this.DbConnector.Attach("TY_P_UT_6CQEH217", Get_Numeric(fsCHMTQTY.ToString()),                     // 이전 출고량
                                                        "0",                                                   // 현 출고량
                                                        Get_Date(this.DTP01_CHIPHANG.GetValue().ToString()),   // 입항일자
                                                        this.CBH01_CHBONSUN.GetValue().ToString(),             // 본선
                                                        this.CBH01_CHHWAJU.GetValue().ToString(),              // 화주
                                                        this.CBH01_CHHWAMUL.GetValue().ToString(),             // 화물
                                                        this.TXT01_CHIPTANK.GetValue().ToString().Trim()       // 입고탱크
                                                        );

            #endregion

            #region Description : SURVEY 파일

            if (this.CBO01_CHDANYI.GetValue().ToString() != "2")
            {
                this.DbConnector.Attach("TY_P_UT_6CQEK218", Get_Numeric(fsCHMTQTY.ToString()),                     // 이전 출고량
                                                            "0",                                                   // 현 출고량
                                                            Get_Date(this.DTP01_CHIPHANG.GetValue().ToString()),   // 입항일자
                                                            this.CBH01_CHBONSUN.GetValue().ToString(),             // 본선
                                                            this.CBH01_CHHWAJU.GetValue().ToString(),              // 화주
                                                            this.CBH01_CHHWAMUL.GetValue().ToString(),             // 화물
                                                            this.TXT01_CHCHTANK.GetValue().ToString().Trim()       // 출고탱크
                                                            );
            }

            #endregion

            #region Description : B/L별 입고파일

            this.DbConnector.Attach("TY_P_UT_6CQES219", Get_Numeric(fsCHMTQTY.ToString()),                     // 이전 출고량
                                                        "0",                                                   // 현 출고량
                                                        Get_Date(this.DTP01_CHIPHANG.GetValue().ToString()),   // 입항일자
                                                        this.CBH01_CHBONSUN.GetValue().ToString(),             // 본선
                                                        this.CBH01_CHHWAJU.GetValue().ToString(),              // 화주
                                                        this.CBH01_CHHWAMUL.GetValue().ToString(),             // 화물
                                                        this.TXT01_CHBLNO.GetValue().ToString(),               // BL번호
                                                        this.TXT01_CHMSNSEQ.GetValue().ToString(),             // MSN번호
                                                        this.TXT01_CHHSNSEQ.GetValue().ToString()              // HSN번호
                                                        );

            #endregion

            #region Description : 통관파일

            this.DbConnector.Attach("TY_P_UT_6CQEX220", Get_Numeric(fsCHMTQTY.ToString()),                     // 이전 출고량
                                                       "0",                                                    // 현 출고량
                                                        Get_Date(this.DTP01_CHIPHANG.GetValue().ToString()),   // 입항일자
                                                        this.CBH01_CHBONSUN.GetValue().ToString(),             // 본선
                                                        this.CBH01_CHHWAJU.GetValue().ToString(),              // 화주
                                                        this.CBH01_CHHWAMUL.GetValue().ToString(),             // 화물
                                                        this.TXT01_CHBLNO.GetValue().ToString(),               // BL번호
                                                        this.TXT01_CHMSNSEQ.GetValue().ToString(),             // MSN번호
                                                        this.TXT01_CHHSNSEQ.GetValue().ToString(),             // HSN번호
                                                        Get_Date(this.DTP01_CHCUSTIL.GetValue().ToString()),   // 통관일자
                                                        this.TXT01_CHCHASU.GetValue().ToString()               // 통관차수
                                                        );

            #endregion

            #region Description : 통관화주별 재고 파일

            string sCHMTQTY = string.Empty;

            sCHMTQTY = "0";
            fsCHYNCHQTY_AFT = "0";

            if (this.CBH01_CHYSHWAJU.GetValue().ToString() != "" && this.CBH01_CHYDHWAJU.GetValue().ToString() != "" &&
                Get_Date(this.DTP01_CHYSDATE.GetValue().ToString().Trim()) != "0" && Get_Numeric(this.TXT01_CHYDSEQ.GetValue().ToString()) != "0" &&
                Get_Numeric(this.TXT01_CHYSSEQ.GetValue().ToString()) != "0")
            {
                fsCHYNGUBUN = "R";

                fsCHMTQTY = "0";

                fsCHYNCHQTY_AFT = this.TXT01_CHMTQTY.GetValue().ToString();
            }
            else
            {
                sCHMTQTY = Get_Numeric(this.TXT01_CHMTQTY.GetValue().ToString());
            }

            this.DbConnector.Attach("TY_P_UT_6CQF8221", fsCHYNCHQTY_AGO,                                       // 이전 양수출고량
                                                        "0",                                                   // 이후 양수출고량
                                                        fsCHMTQTY,                                             // 이전 출고량
                                                        "0",                                                   // 현   출고량
                                                        fsCHYNCHQTY_AGO,                                       // 이전 양수출고량
                                                        fsCHMTQTY,                                             // 이전 출고량
                                                        "0",                                                   // 이후 양수출고량
                                                        "0",                                                   // 현   출고량
                                                        this.CBH01_CHACTHJ.GetValue().ToString(),              // 통관화주
                                                        this.TXT01_CHJGHWAJU.GetValue().ToString(),            // 재고화주
                                                        Get_Date(this.DTP01_CHIPHANG.GetValue().ToString()),   // 입항일자
                                                        this.CBH01_CHBONSUN.GetValue().ToString(),             // 본선
                                                        this.CBH01_CHHWAJU.GetValue().ToString(),              // 화주
                                                        this.CBH01_CHHWAMUL.GetValue().ToString(),             // 화물
                                                        this.TXT01_CHBLNO.GetValue().ToString(),               // BL번호
                                                        this.TXT01_CHMSNSEQ.GetValue().ToString(),             // MSN번호
                                                        this.TXT01_CHHSNSEQ.GetValue().ToString(),             // HSN번호
                                                        Get_Date(this.DTP01_CHCUSTIL.GetValue().ToString()),   // 통관일자
                                                        this.TXT01_CHCHASU.GetValue().ToString(),              // 통관차수
                                                        this.CBH01_CHYSHWAJU.GetValue().ToString(),            // 양수화주
                                                        this.CBH01_CHYDHWAJU.GetValue().ToString(),            // 양도화주
                                                        Get_Date(this.DTP01_CHYSDATE.GetValue().ToString()),   // 양수일자
                                                        this.TXT01_CHYDSEQ.GetValue().ToString(),              // 양도차수
                                                        this.TXT01_CHYSSEQ.GetValue().ToString()               // 양수순번
                                                        );

            #endregion

            #region Description : 양수도 파일

            if (this.CBH01_CHYSHWAJU.GetValue().ToString() != "" && this.CBH01_CHYDHWAJU.GetValue().ToString() != "" &&
               Get_Date(this.DTP01_CHYSDATE.GetValue().ToString().Trim()) != "0" && Get_Numeric(this.TXT01_CHYDSEQ.GetValue().ToString()) != "0" &&
               Get_Numeric(this.TXT01_CHYSSEQ.GetValue().ToString()) != "0")
            {
                this.DbConnector.Attach("TY_P_UT_6CQFZ222", fsCHYNCHQTY_AGO.ToString(),                            // 이전 양수출고량
                                                            "0",                                                   // 이후 양수출고량
                                                            fsCHYNCHQTY_AGO.ToString(),                            // 이전 양수출고량
                                                            "0",                                                   // 이후 양수출고량
                                                            Get_Date(this.DTP01_CHIPHANG.GetValue().ToString()),   // 입항일자
                                                            this.CBH01_CHBONSUN.GetValue().ToString(),             // 본선
                                                            this.CBH01_CHHWAJU.GetValue().ToString(),              // 화주
                                                            this.CBH01_CHHWAMUL.GetValue().ToString(),             // 화물
                                                            this.TXT01_CHBLNO.GetValue().ToString(),               // BL번호
                                                            this.TXT01_CHMSNSEQ.GetValue().ToString(),             // MSN번호
                                                            this.TXT01_CHHSNSEQ.GetValue().ToString(),             // HSN번호
                                                            Get_Date(this.DTP01_CHCUSTIL.GetValue().ToString()),   // 통관일자
                                                            this.TXT01_CHCHASU.GetValue().ToString(),              // 통관차수
                                                            this.CBH01_CHACTHJ.GetValue().ToString(),              // 통관화주
                                                            this.CBH01_CHYDHWAJU.GetValue().ToString(),            // 양도화주
                                                            this.CBH01_CHYSHWAJU.GetValue().ToString(),            // 양수화주
                                                            Get_Date(this.DTP01_CHYSDATE.GetValue().ToString()),   // 양수일자
                                                            this.TXT01_CHYDSEQ.GetValue().ToString(),              // 양도차수
                                                            this.TXT01_CHYSSEQ.GetValue().ToString()               // 양수순번
                                                            );
            }

            #endregion

            #region Description : 일자별 DRUM 출고파일

            if (this.CBO01_CHDANYI.GetValue().ToString() == "2")
            {
                // 일자별 DRUM 출고파일 업데이트
                this.DbConnector.Attach("TY_P_UT_6CQCY207", fsCHDRQTY.ToString(),                                  // 이전 드럼개수
                                                            "0",                                                   // 이후 드럼개수
                                                            fsCHMTQTY.ToString(),                                  // 이전 MT량
                                                            "0",                                                   // 이후 MT량
                                                            fsCHKLQTY.ToString(),                                  // 이전 KL량                                                            
                                                            "0",                                                   // 이후 KL량
                                                            Get_Date(this.DTP01_CHCHULIL.GetValue().ToString()),   // 출고일자
                                                            this.CBH01_CHACTHJ.GetValue().ToString(),              // 통관화주
                                                            Get_Date(this.DTP01_CHIPHANG.GetValue().ToString()),   // 입항일자
                                                            this.CBH01_CHBONSUN.GetValue().ToString(),             // 본선
                                                            this.CBH01_CHHWAJU.GetValue().ToString(),              // 화주
                                                            this.CBH01_CHHWAMUL.GetValue().ToString(),             // 화물
                                                            this.TXT01_CHBLNO.GetValue().ToString(),               // BL번호
                                                            this.TXT01_CHMSNSEQ.GetValue().ToString(),             // MSN번호
                                                            this.TXT01_CHHSNSEQ.GetValue().ToString(),             // HSN번호
                                                            Get_Date(this.DTP01_CHCUSTIL.GetValue().ToString()),   // 통관일자
                                                            this.TXT01_CHCHASU.GetValue().ToString(),              // 통관차수
                                                            this.TXT01_CHJGHWAJU.GetValue().ToString(),            // 재고화주
                                                            this.CBH01_CHYSHWAJU.GetValue().ToString(),            // 양수화주
                                                            this.CBH01_CHYDHWAJU.GetValue().ToString(),            // 양도화주
                                                            Get_Date(this.DTP01_CHYSDATE.GetValue().ToString()),   // 양수일자
                                                            this.TXT01_CHYDSEQ.GetValue().ToString(),              // 양도차수
                                                            this.TXT01_CHYSSEQ.GetValue().ToString(),              // 양수순번
                                                            this.TXT01_CHJUNG.GetValue().ToString()                // 중량
                                                    );
            }

            #endregion

            #region Description : 출고지시 및 DRUM 지시

            // 케미컬 출고일 경우
            if (this.CBO01_CHDANYI.GetValue().ToString() != "2")
            {
                if (this.TXT01_CHJISINUM1.GetValue().ToString() != "" &&
                    this.TXT01_CHJISINUM2.GetValue().ToString() != "")
                {
                    // 출고 지시
                    this.DbConnector.Attach("TY_P_UT_6CR9Y228", fsCHMTQTY.ToString(),                                  // 이전 출고량
                                                                "0",                                                   // 현   출고량
                                                                fsCHMTQTY.ToString(),                                  // 이전 출고량
                                                                "0",                                                   // 현   출고량
                                                                this.TXT01_CHJISINUM1.GetValue().ToString(),           // 지시일자
                                                                this.TXT01_CHJISINUM2.GetValue().ToString()            // 지시순번
                                                                );
                }
            }
            else
            {
                if (sJIDR_GUBUN == "UPT")
                {
                    // DRUM 지시
                    this.DbConnector.Attach("TY_P_UT_6CRED233", fsCHDRQTY.ToString(),                                  // 이전 DRUM 개수
                                                                "0",                                                   // 이후 DRUM 개수
                                                                fsCHDRQTY.ToString(),                                  // 이전 DRUM 개수
                                                                "0",                                                   // 이후 DRUM 개수
                                                                fsCHDRQTY.ToString(),                                  // 이전 DRUM 개수
                                                                "0",                                                   // 이후 DRUM 개수
                                                                this.TXT01_CHJISINUM1.GetValue().ToString(),           // 지시일자
                                                                this.TXT01_CHJISINUM2.GetValue().ToString(),           // 지시순번
                                                                Get_Date(this.DTP01_CHIPHANG.GetValue().ToString().Trim()),
                                                                this.CBH01_CHBONSUN.GetValue().ToString().Trim().ToUpper(),
                                                                this.CBH01_CHHWAJU.GetValue().ToString().Trim().ToUpper(),
                                                                this.CBH01_CHHWAMUL.GetValue().ToString().Trim().ToUpper(),
                                                                this.TXT01_CHBLNO.GetValue().ToString().Trim(),
                                                                Get_Numeric(this.TXT01_CHMSNSEQ.GetValue().ToString().Trim()),
                                                                Get_Numeric(this.TXT01_CHHSNSEQ.GetValue().ToString().Trim()),
                                                                this.CBH01_CHACTHJ.GetValue().ToString().Trim().ToUpper(),
                                                                Get_Date(this.DTP01_CHCUSTIL.GetValue().ToString().Trim()),
                                                                Get_Numeric(this.TXT01_CHCHASU.GetValue().ToString().Trim()),
                                                                this.TXT01_CHJGHWAJU.GetValue().ToString(),
                                                                this.CBH01_CHYSHWAJU.GetValue().ToString(),
                                                                this.CBH01_CHYDHWAJU.GetValue().ToString(),
                                                                Get_Date(this.DTP01_CHYSDATE.GetValue().ToString().Trim()),
                                                                this.TXT01_CHYDSEQ.GetValue().ToString(),
                                                                this.TXT01_CHYSSEQ.GetValue().ToString()
                                                                );
                }
            }

            #endregion

            #region Description : 드럼 출고

            if (this.CBO01_CHDANYI.GetValue().ToString() == "2")
            {
                if (sDRUJ_GUBUN == "UPT")
                {
                    // DRUM 재고
                    this.DbConnector.Attach("TY_P_UT_6CREZ234", fsCHDRQTY.ToString(),                                  // 이전 DRUM 개수
                                                                "0",                                                   // 이후 DRUM 개수
                                                                fsCHDRQTY.ToString(),                                  // 이전 DRUM 개수
                                                                "0",                                                   // 이후 DRUM 개수
                                                                this.CBH01_CHACTHJ.GetValue().ToString().Trim(),
                                                                this.CBH01_CHHWAMUL.GetValue().ToString().Trim(),
                                                                Get_Date(this.DTP01_CHIPHANG.GetValue().ToString().Trim()),
                                                                this.CBH01_CHBONSUN.GetValue().ToString().Trim(),
                                                                this.CBH01_CHHWAJU.GetValue().ToString().Trim(),
                                                                this.TXT01_CHBLNO.GetValue().ToString().Trim(),
                                                                Get_Numeric(this.TXT01_CHMSNSEQ.GetValue().ToString().Trim()),
                                                                Get_Numeric(this.TXT01_CHHSNSEQ.GetValue().ToString().Trim()),
                                                                Get_Date(this.DTP01_CHCUSTIL.GetValue().ToString().Trim()),
                                                                Get_Numeric(this.TXT01_CHCHASU.GetValue().ToString().Trim()),
                                                                this.TXT01_CHJGHWAJU.GetValue().ToString(),
                                                                this.CBH01_CHYSHWAJU.GetValue().ToString(),
                                                                this.CBH01_CHYDHWAJU.GetValue().ToString(),
                                                                Get_Date(this.DTP01_CHYSDATE.GetValue().ToString().Trim()),
                                                                this.TXT01_CHYDSEQ.GetValue().ToString(),
                                                                this.TXT01_CHYSSEQ.GetValue().ToString(),
                                                                Get_Numeric(this.TXT01_CHJUNG.GetValue().ToString().Trim())
                                                                );
                }

                if (sDRUT_GUBUN == "UPT")
                {
                    // 탱크별 DRUM 재고
                    this.DbConnector.Attach("TY_P_UT_6CRF3235", fsCHDRQTY.ToString(),                                  // 이전 DRUM 개수
                                                                "0",                                                   // 이후 DRUM 개수
                                                                fsCHDRQTY.ToString(),                                  // 이전 DRUM 개수
                                                                "0",                                                   // 이후 DRUM 개수
                                                                this.CBH01_CHACTHJ.GetValue().ToString().Trim(),
                                                                this.CBH01_CHHWAMUL.GetValue().ToString().Trim(),
                                                                Get_Date(this.DTP01_CHIPHANG.GetValue().ToString().Trim()),
                                                                this.CBH01_CHBONSUN.GetValue().ToString().Trim(),
                                                                this.CBH01_CHHWAJU.GetValue().ToString().Trim(),
                                                                this.TXT01_CHCHTANK.GetValue().ToString().Trim(),
                                                                Get_Numeric(this.TXT01_CHJUNG.GetValue().ToString().Trim())
                                                                );
                }
            }

            #endregion

            this.DbConnector.ExecuteTranQueryList();

            UP_BUTTON_Visible("");

            this.BTN61_INQ_Click(null, null);

            this.ShowMessage("TY_M_GB_23NAD874");
        }
        #endregion

        #region Description : 통관화주파일 등록
        private void UP_UTICUHJF_INS()
        {


        }
        #endregion

        #region Description : 통관화주파일 삭제
        private void UP_UTICUHJF_DEL()
        {
        }
        #endregion

        #region Description : 조회 메소드
        private void UP_SEARCH()
        {
            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_83DBY693",
                this.TXT01_CHTKNO1.GetValue().ToString(),
                Get_Date(this.DTP01_STIPHANG.GetValue().ToString()),
                Get_Date(this.DTP01_EDIPHANG.GetValue().ToString()),
                this.TXT01_CHCARNO1.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_UT_6CLD2141.SetValue(dt);
        }
        #endregion

        #region Description : 확인 메소드
        private void UP_RUN()
        {
            fsCHYNCHQTY_AGO = "0";

            UP_Set_ReadOnly("OK");

            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_6CLD4142",
                Get_Date(this.DTP01_CHCHULIL.GetValue().ToString()), // 출고일자
                this.TXT01_CHTKNO.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.CurrentDataTableRowMapping(dt, "01");

                fsCHIPHANG = dt.Rows[0]["CHIPHANG"].ToString();
                fsCHBONSUN = dt.Rows[0]["CHBONSUN"].ToString();
                fsCHHWAJU = dt.Rows[0]["CHHWAJU"].ToString();
                fsCHHWAMUL = dt.Rows[0]["CHHWAMUL"].ToString();
                fsCHBLNO = dt.Rows[0]["CHBLNO"].ToString();
                fsCHMSNSEQ = dt.Rows[0]["CHMSNSEQ"].ToString();
                fsCHHSNSEQ = dt.Rows[0]["CHHSNSEQ"].ToString();
                fsCHACTHJ = dt.Rows[0]["CHACTHJ"].ToString();
                fsCHCUSTIL = dt.Rows[0]["CHCUSTIL"].ToString();
                fsCHCHASU = dt.Rows[0]["CHCHASU"].ToString();

                fsCHJGHWAJU = dt.Rows[0]["CHJGHWAJU"].ToString();
                fsCHYSHWAJU = dt.Rows[0]["CHYSHWAJU"].ToString();
                fsCHYDHWAJU = dt.Rows[0]["CHYDHWAJU"].ToString();
                fsCHYSDATE = dt.Rows[0]["CHYSDATE"].ToString();
                fsCHYDSEQ = dt.Rows[0]["CHYDSEQ"].ToString();
                fsCHYSSEQ = dt.Rows[0]["CHYSSEQ"].ToString();

                fsCHCHTANK = dt.Rows[0]["CHCHTANK"].ToString();
                fsCHIPTANK = dt.Rows[0]["CHIPTANK"].ToString();
                fsCHCHULGB = dt.Rows[0]["CHCHULGB"].ToString();
                fsCHMTQTY = dt.Rows[0]["CHMTQTY"].ToString();
                fsCHYNCHQTY_AGO = dt.Rows[0]["CHYNCHQTY"].ToString();
                fsCHKLQTY = dt.Rows[0]["CHKLQTY"].ToString();
                fsCHDANYI = dt.Rows[0]["CHDANYI"].ToString();
                fsCHJUNG = dt.Rows[0]["CHJUNG"].ToString();
                fsCHDRQTY = dt.Rows[0]["CHQTY"].ToString();
                fsCHCHHJ = dt.Rows[0]["CHCHHJ"].ToString();
                fsCHHWAPE = dt.Rows[0]["CHHWAPE"].ToString();
                fsCHJISINUM = dt.Rows[0]["CHJISINUM"].ToString();
                fsCHHIGB = dt.Rows[0]["CHHIGB"].ToString();

                fsGUBUN = "UPT";

                UP_BUTTON_Visible(fsGUBUN);
            }
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
                Get_Date(this.DTP01_CHCHULIL.GetValue().ToString()), // 입항일자
                this.TXT01_CHTKNO.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {

            }
        }
        #endregion

        #region Description : 통관화주파일 조회
        private void UP_UTICUHJF_SEARCH()
        {
            //if (fsCJYDHWAJU.ToString() == "" && fsCJYSDATE.ToString() == "0" && fsCJYDSEQ.ToString() == "0" && fsCJYSSEQ.ToString() == "0")
            //{
            //    fsCJYSHWAJU = "";
            //}
            //else
            //{
            //    fsCJYSHWAJU = this.CBH01_CHYDHWAJU.GetValue().ToString();
            //}

            //DataTable dt = new DataTable();

            //this.DbConnector.CommandClear();
            //this.DbConnector.Attach
            //    (
            //    "TY_P_UT_69JGC164",
            //    this.CBH01_CHACTHJ.GetValue().ToString(),            // 통관화주
            //    Get_Date(this.DTP01_CHIPHANG.GetValue().ToString()), // 입항일자
            //    this.CBH01_CHBONSUN.GetValue().ToString(),           // 본선
            //    this.CBH01_CHHWAJU.GetValue().ToString(),            // 화주
            //    this.CBH01_CHHWAMUL.GetValue().ToString(),           // 화물
            //    this.TXT01_YNBLNO.GetValue().ToString(),             // BL번호
            //    this.TXT01_YNMSNSEQ.GetValue().ToString(),           // MSN번호
            //    this.TXT01_YNHSNSEQ.GetValue().ToString(),           // HSN번호
            //    Get_Date(this.DTP01_CHCUSTIL.GetValue().ToString()), // 통관일자
            //    this.TXT01_CHCHASU.GetValue().ToString(),            // 통관차수
            //    fsCJYSHWAJU.ToString(),                              // 양수화주
            //    fsCJYDHWAJU.ToString(),                              // 양도화주
            //    fsCJYSDATE.ToString(),                               // 양수일자
            //    fsCJYDSEQ.ToString(),                                // 양도차수
            //    fsCJYSSEQ.ToString()                                 // 양수순번
            //    );

            //dt = this.DbConnector.ExecuteDataTable();

            //if (dt.Rows.Count > 0)
            //{
            //    // 통관량
            //    this.TXT01_CJCUQTY.SetValue(dt.Rows[0]["CJCUQTY"].ToString());
            //    // 양수량
            //    this.TXT01_CJYSQTY.SetValue(dt.Rows[0]["CJYSQTY"].ToString());
            //    // 양도량
            //    this.TXT01_CJYDQTY.SetValue(dt.Rows[0]["CJYDQTY"].ToString());
            //    // 양수양도량
            //    this.TXT01_CJYSYDQTY.SetValue(dt.Rows[0]["CJYSYDQTY"].ToString());
            //    // 양수출고량
            //    this.TXT01_CJYSCHQTY.SetValue(dt.Rows[0]["CJYSCHQTY"].ToString());
            //    // 출고량
            //    this.TXT01_CJCHQTY.SetValue(dt.Rows[0]["CJCHQTY"].ToString());
            //    // 재고량
            //    this.TXT01_CJJEQTY.SetValue(dt.Rows[0]["CJJEQTY"].ToString());
            //}
        }
        #endregion

        #region Description : 재고 조회(통관 및 양수도)
        private void UP_CALL_JEGO()
        {
            TYUTGB008S popup = new TYUTGB008S(this.TXT01_CHJGHWAJU.GetValue().ToString());

            if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.TXT01_CHJGHWAJU.SetValue(popup.fsJGHWAJU);     // 재고화주
                this.TXT01_CHJGHWAJUNM.SetValue(popup.fsJGHWAJUNM); // 재고화주명
                this.DTP01_CHIPHANG.SetValue(popup.fsIPHANG);       // 입항일자
                this.CBH01_CHBONSUN.SetValue(popup.fsBONSUN);       // 본선
                this.CBH01_CHHWAJU.SetValue(popup.fsHWAJU);         // 화주
                this.CBH01_CHHWAMUL.SetValue(popup.fsHWAMUL);       // 화물
                this.TXT01_CHBLNO.SetValue(popup.fsBLNO);           // BL번호
                this.TXT01_CHMSNSEQ.SetValue(popup.fsMSNSEQ);       // MSN번호
                this.TXT01_CHHSNSEQ.SetValue(popup.fsHSNSEQ);       // HSN번호
                this.DTP01_CHCUSTIL.SetValue(popup.fsCUSTIL);       // 통관일자
                this.TXT01_CHCHASU.SetValue(popup.fsCHASU);         // 통관차수
                this.CBH01_CHACTHJ.SetValue(popup.fsACTHJ);         // 통관화주
                this.CBH01_CHYSHWAJU.SetValue(popup.fsYSHWAJU);     // 양수화주
                this.CBH01_CHYDHWAJU.SetValue(popup.fsYDHWAJU);     // 양도화주
                this.DTP01_CHYSDATE.SetValue(popup.fsYSDATE);       // 양수일자
                this.TXT01_CHYDSEQ.SetValue(popup.fsYDSEQ);         // 양도순번
                this.TXT01_CHYSSEQ.SetValue(popup.fsYSSEQ);         // 양수순번

                this.TXT01_CHCHTANK.SetValue(popup.fsTANKNO);       // 출고탱크
                this.TXT01_CJJEQTY.SetValue(popup.fsCJJEQTY);       // 재고수량

                this.TXT01_DPJEQTY.SetValue(popup.fsDPJEQTY);       // 드럼재고

                // 출하장 가져오기
                UP_GET_CHCHJANG();

                SetFocus(this.TXT01_CHJGHWAJU);
            }
        }
        #endregion

        #region Description : 저장 ProcessCheck
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            int i = 0;

            fsCHIPHANG = "";
            fsCHBONSUN = "";
            fsCHHWAJU = "";
            fsCHHWAMUL = "";
            fsCHBLNO = "";
            fsCHMSNSEQ = "";
            fsCHHSNSEQ = "";
            fsCHACTHJ = "";
            fsCHCUSTIL = "";
            fsCHCHASU = "";
            fsCHCHTANK = "";
            fsCHJGHWAJU = "";
            fsCHYSHWAJU = "";
            fsCHYDHWAJU = "";
            fsCHYSDATE = "";
            fsCHYDSEQ = "";
            fsCHYSSEQ = "";
            fsCHIPTANK = "";

            fsSVMTQTY = "";
            fsSVCHQTY = "";
            fsSVKLQTY = "";
            fsSVBIJUNG = "";

            fsKLQTY = "";
            fsMTQTY = "";

            fsDJCHQTY = "";
            fsDJPOQTY = "";
            fsDJJEQTY = "";

            fsDTCHQTY = "";
            fsDTPOQTY = "";
            fsDTJEQTY = "";

            fsCONVERT = "";
            fsOVER_KL = "";
            fsKESAN = "";
            fsCHHWAPE = "";
            fsCHMTQTY = "0";
            fsCHYNCHQTY_AGO = "0";
            fsCHKLQTY = "0";
            fsCHJISINUM = "";
            fsCHQTY = "0";

            fsCHYNGUBUN = "";
            fsCHYNCHQTY_AFT = "0";

            fsCHJUNG = "";
            fsCHCHHJ = "";
            fsCHBIJUNG = "";
            fsCHVCF = "";
            fsSCCHJEQTY = "";
            fsSCIPJEQTY = "";
            fsCHDANYI = "";

            fsCHCHULGB = "";

            DataTable dt = new DataTable();

            if (this.CBO01_CHDANYI.GetValue().ToString() == "1")
            {
                if (double.Parse(Get_Numeric(this.TXT01_CHMTQTY.GetValue().ToString())) == 0)
                {
                    this.ShowMessage("TY_M_UT_6CT91262");
                    this.TXT01_CHMTQTY.Focus();

                    e.Successed = false;
                    return;
                }
            }

            if (this.CBO01_CHDANYI.GetValue().ToString() == "2") // 드럼
            {
                if (double.Parse(Get_Numeric(this.TXT01_CHMTQTY.GetValue().ToString().Trim())) == 0)
                {
                    this.TXT01_CHMTQTY.SetValue(
                        (
                           double.Parse(String.Format("{0,9:N3}", Get_Numeric(this.TXT01_CHJUNG.GetValue().ToString().Trim())))
                         * double.Parse(String.Format("{0,9:N3}", Get_Numeric(this.TXT01_CHQTY.GetValue().ToString().Trim())))
                        ).ToString("0.000"));
                }
            }
            else
            {
                if (double.Parse(Get_Numeric(this.TXT01_CHJUNG.GetValue().ToString().Trim())) != 0)
                {
                    this.ShowMessage("TY_M_UT_82CEH607");
                    this.TXT01_CHJUNG.Focus();

                    e.Successed = false;
                    return;
                }

                if (double.Parse(Get_Numeric(this.TXT01_CHQTY.GetValue().ToString().Trim())) != 0)
                {
                    this.ShowMessage("TY_M_UT_82CEI608");
                    this.TXT01_CHQTY.Focus();

                    e.Successed = false;
                    return;
                }

            }

            if (this.CBO01_CHWKTYPE.GetValue().ToString() == "02" || this.CBO01_CHWKTYPE.GetValue().ToString() == "03")
            {
                //if (this.TXT01_CHCONTNUM.GetValue().ToString().Length != 11)
                //{
                //    this.ShowMessage("TY_M_UT_7AHHI835");
                //    SetFocus(this.TXT01_CHCONTNUM);

                //    e.Successed = false;
                //    return;
                //}
            }

            if (this.CBO01_CHWKTYPE.GetValue().ToString() == "03")
            {
                if (this.CBH01_CHDNST.GetValue().ToString() == "" && this.TXT01_CHJGHWAJU.GetValue().ToString() == "GSG")
                {
                    this.ShowMessage("TY_M_UT_81JB1490");
                    SetFocus(this.CBH01_CHDNST.CodeText);

                    e.Successed = false;
                    return;
                }
            }

            if (this.CBO01_CHWKTYPE.GetValue().ToString() == "02" || this.CBO01_CHWKTYPE.GetValue().ToString() == "03")
            {
                if (this.TXT01_CHCONTNUM.GetValue().ToString() != "")
                {
                    string sCONTNUM = string.Empty;

                    sCONTNUM = this.TXT01_CHCONTNUM.GetValue().ToString();

                    for (i = 0; i < 11; i++)
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
                                SetFocus(this.TXT01_CHCONTNUM);

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
                                SetFocus(this.TXT01_CHCONTNUM);

                                e.Successed = false;
                                return;
                            }
                        }
                    }
                }
            }

            if (fsGUBUN == "UPT")
            {
                fsCHYNCHQTY_AGO = "0";

                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_UT_6CLD4142",
                    Get_Date(this.DTP01_CHCHULIL.GetValue().ToString()), // 출고일자
                    this.TXT01_CHTKNO.GetValue().ToString()
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    fsCHIPHANG = dt.Rows[0]["CHIPHANG"].ToString();
                    fsCHBONSUN = dt.Rows[0]["CHBONSUN"].ToString();
                    fsCHHWAJU = dt.Rows[0]["CHHWAJU"].ToString();
                    fsCHHWAMUL = dt.Rows[0]["CHHWAMUL"].ToString();
                    fsCHBLNO = dt.Rows[0]["CHBLNO"].ToString();
                    fsCHMSNSEQ = dt.Rows[0]["CHMSNSEQ"].ToString();
                    fsCHHSNSEQ = dt.Rows[0]["CHHSNSEQ"].ToString();
                    fsCHACTHJ = dt.Rows[0]["CHACTHJ"].ToString();
                    fsCHCUSTIL = dt.Rows[0]["CHCUSTIL"].ToString();
                    fsCHCHASU = dt.Rows[0]["CHCHASU"].ToString();

                    fsCHJGHWAJU = dt.Rows[0]["CHJGHWAJU"].ToString();
                    fsCHYSHWAJU = dt.Rows[0]["CHYSHWAJU"].ToString();
                    fsCHYDHWAJU = dt.Rows[0]["CHYDHWAJU"].ToString();
                    fsCHYSDATE = dt.Rows[0]["CHYSDATE"].ToString();
                    fsCHYDSEQ = dt.Rows[0]["CHYDSEQ"].ToString();
                    fsCHYSSEQ = dt.Rows[0]["CHYSSEQ"].ToString();

                    fsCHCHTANK = dt.Rows[0]["CHCHTANK"].ToString();
                    fsCHIPTANK = dt.Rows[0]["CHIPTANK"].ToString();
                    fsCHCHULGB = dt.Rows[0]["CHCHULGB"].ToString();
                    fsCHMTQTY = dt.Rows[0]["CHMTQTY"].ToString();
                    fsCHYNCHQTY_AGO = dt.Rows[0]["CHYNCHQTY"].ToString();
                    fsCHKLQTY = dt.Rows[0]["CHKLQTY"].ToString();
                    fsCHDANYI = dt.Rows[0]["CHDANYI"].ToString();
                    fsCHJUNG = dt.Rows[0]["CHJUNG"].ToString();
                    fsCHDRQTY = dt.Rows[0]["CHQTY"].ToString();
                    fsCHJUNG = dt.Rows[0]["CHJUNG"].ToString();
                    fsCHCHHJ = dt.Rows[0]["CHCHHJ"].ToString();
                    fsCHHWAPE = dt.Rows[0]["CHHWAPE"].ToString();
                    fsCHJISINUM = dt.Rows[0]["CHJISINUM"].ToString();
                    fsCHHIGB = dt.Rows[0]["CHHIGB"].ToString();
                    fsCHCHTANK = dt.Rows[0]["CHCHTANK"].ToString();
                    fsCHIPTANK = dt.Rows[0]["CHIPTANK"].ToString();
                    fsCHQTY = dt.Rows[0]["CHQTY"].ToString();
                }
            }


            #region Description : DRUM 체크

            if (this.CBO01_CHDANYI.GetValue().ToString() == "2")
            {
                if (double.Parse(Get_Numeric(this.TXT01_CHJUNG.GetValue().ToString())) <= 0)
                {
                    this.ShowMessage("TY_M_UT_6CLFR143");
                    this.TXT01_CHJUNG.Focus();

                    e.Successed = false;
                    return;
                }

                if (double.Parse(Get_Numeric(this.TXT01_CHQTY.GetValue().ToString())) <= 0)
                {
                    this.ShowMessage("TY_M_UT_6CLFR143");
                    this.TXT01_CHJUNG.Focus();

                    e.Successed = false;
                    return;
                }

                if (fsGUBUN.ToString() == "UPT")
                {
                    if (Get_Numeric(this.TXT01_CHJUNG.GetValue().ToString()) != Get_Numeric(fsCHJUNG.ToString()))
                    {
                        this.ShowMessage("TY_M_UT_6CLFS144");
                        this.TXT01_CHJUNG.Focus();

                        e.Successed = false;
                        return;
                    }
                }


                // 20180112 박선형 주임 DRUM이 수출용이기 때문에 지시를 안 내리고 포장 후 출고가능하도록 해달라고 요청
                // 20180112 드럼 지시와 관련된 내용(재고등) 관리 못해준다고 통보

                if (Get_Date(this.TXT01_CHJISINUM1.GetValue().ToString().Trim()) != "")
                {
                    // 지시 내용과 일치하는지 체크
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_UT_6CMHC190",
                        Get_Date(this.TXT01_CHJISINUM1.GetValue().ToString().Trim()),
                        this.TXT01_CHJISINUM2.GetValue().ToString().Trim(),
                        Get_Date(this.DTP01_CHIPHANG.GetValue().ToString().Trim()),
                        this.CBH01_CHBONSUN.GetValue().ToString().Trim().ToUpper(),
                        this.CBH01_CHHWAJU.GetValue().ToString().Trim().ToUpper(),
                        this.CBH01_CHHWAMUL.GetValue().ToString().Trim().ToUpper(),
                        this.TXT01_CHBLNO.GetValue().ToString().Trim(),
                        Get_Numeric(this.TXT01_CHMSNSEQ.GetValue().ToString().Trim()),
                        Get_Numeric(this.TXT01_CHHSNSEQ.GetValue().ToString().Trim()),
                        this.CBH01_CHACTHJ.GetValue().ToString().Trim().ToUpper(),
                        Get_Date(this.DTP01_CHCUSTIL.GetValue().ToString().Trim()),
                        Get_Numeric(this.TXT01_CHCHASU.GetValue().ToString().Trim()),
                        this.TXT01_CHJGHWAJU.GetValue().ToString(),
                        this.CBH01_CHYSHWAJU.GetValue().ToString(),
                        this.CBH01_CHYDHWAJU.GetValue().ToString(),
                        Get_Date(this.DTP01_CHYSDATE.GetValue().ToString().Trim()),
                        this.TXT01_CHYDSEQ.GetValue().ToString(),
                        this.TXT01_CHYSSEQ.GetValue().ToString()
                        );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count <= 0)
                    {
                        this.ShowMessage("TY_M_UT_6CMHA188");
                        SetFocus(this.TXT01_CHJGHWAJU);

                        e.Successed = false;
                        return;
                    }
                    else
                    {
                        double dJDJIQTY = 0;

                        dJDJIQTY = double.Parse(String.Format("{0,9:N0}", dt.Rows[0]["JDJIQTY"].ToString()));

                        if (double.Parse(String.Format("{0,9:N0}", dt.Rows[0]["JDJIQTY"].ToString())) < double.Parse(String.Format("{0,9:N0}", Get_Numeric(this.TXT01_CHQTY.GetValue().ToString()))))
                        {
                            this.ShowMessage("TY_M_UT_7CKDU344");
                            SetFocus(this.TXT01_CHJISINUM1);

                            e.Successed = false;
                            return;
                        }

                        double dCHQTY = 0;
                        string sCHJISINUM = string.Empty;

                        sCHJISINUM = this.TXT01_CHJISINUM1.GetValue().ToString().Trim() + Set_Fill3(this.TXT01_CHJISINUM2.GetValue().ToString().Trim());

                        // 지시 내용과 일치하는지 체크
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach
                            (
                            "TY_P_UT_7CKDW345",
                            sCHJISINUM.ToString(),
                            Get_Date(this.DTP01_CHIPHANG.GetValue().ToString().Trim()),
                            this.CBH01_CHBONSUN.GetValue().ToString().Trim().ToUpper(),
                            this.CBH01_CHHWAJU.GetValue().ToString().Trim().ToUpper(),
                            this.CBH01_CHHWAMUL.GetValue().ToString().Trim().ToUpper(),
                            this.TXT01_CHBLNO.GetValue().ToString().Trim(),
                            Get_Numeric(this.TXT01_CHMSNSEQ.GetValue().ToString().Trim()),
                            Get_Numeric(this.TXT01_CHHSNSEQ.GetValue().ToString().Trim()),
                            Get_Date(this.DTP01_CHCUSTIL.GetValue().ToString().Trim()),
                            Get_Numeric(this.TXT01_CHCHASU.GetValue().ToString().Trim()),
                            this.CBH01_CHACTHJ.GetValue().ToString().Trim().ToUpper(),
                            this.TXT01_CHJGHWAJU.GetValue().ToString(),
                            this.CBH01_CHYSHWAJU.GetValue().ToString(),
                            this.CBH01_CHYDHWAJU.GetValue().ToString(),
                            Get_Date(this.DTP01_CHYSDATE.GetValue().ToString().Trim()),
                            this.TXT01_CHYDSEQ.GetValue().ToString(),
                            this.TXT01_CHYSSEQ.GetValue().ToString(),
                            this.CBH01_CHCHHJ.GetValue().ToString().Trim()
                            );

                        dt = this.DbConnector.ExecuteDataTable();

                        if (dt.Rows.Count > 0)
                        {
                            dCHQTY = double.Parse(String.Format("{0,9:N0}", dt.Rows[0]["CHQTY"].ToString()));
                        }

                        if (dJDJIQTY < dCHQTY + double.Parse(String.Format("{0,9:N0}", Get_Numeric(this.TXT01_CHQTY.GetValue().ToString()))) - double.Parse(String.Format("{0,9:N0}", fsCHQTY.ToString())))
                        {
                            this.ShowMessage("TY_M_UT_7CKE6346");
                            SetFocus(this.TXT01_CHQTY);

                            e.Successed = false;
                            return;
                        }
                    }
                }
                //else
                //{
                //    this.ShowMessage("TY_M_UT_7CKBG339");
                //    SetFocus(this.TXT01_CHJISINUM1);

                //    e.Successed = false;
                //    return;
                //}


                if (fsGUBUN == "UPT")
                {
                    if (Get_Date(this.DTP01_CHIPHANG.GetValue().ToString().Trim()) != fsCHIPHANG.ToString().Trim() ||
                        this.CBH01_CHBONSUN.GetValue().ToString().Trim().ToUpper() != fsCHBONSUN.ToString().Trim() ||
                        this.CBH01_CHHWAJU.GetValue().ToString().Trim().ToUpper() != fsCHHWAJU.ToString().Trim() ||
                        this.CBH01_CHHWAMUL.GetValue().ToString().Trim().ToUpper() != fsCHHWAMUL.ToString().Trim() ||
                        this.TXT01_CHBLNO.GetValue().ToString().Trim() != fsCHBLNO.ToString().Trim() ||
                        Get_Numeric(this.TXT01_CHMSNSEQ.GetValue().ToString().Trim()) != fsCHMSNSEQ.ToString().Trim() ||
                        Get_Numeric(this.TXT01_CHHSNSEQ.GetValue().ToString().Trim()) != fsCHHSNSEQ.ToString().Trim() ||
                        this.CBH01_CHACTHJ.GetValue().ToString() != fsCHACTHJ.ToString().Trim() ||
                        Get_Date(this.DTP01_CHCUSTIL.GetValue().ToString().Trim()) != fsCHCUSTIL.ToString().Trim() ||
                        Get_Numeric(this.TXT01_CHCHASU.GetValue().ToString().Trim()) != fsCHCHASU.ToString().Trim() ||
                        this.TXT01_CHJGHWAJU.GetValue().ToString() != fsCHJGHWAJU.ToString().Trim() ||
                        this.CBH01_CHYSHWAJU.GetValue().ToString() != fsCHYSHWAJU.ToString().Trim() ||
                        this.CBH01_CHYDHWAJU.GetValue().ToString() != fsCHYDHWAJU.ToString().Trim() ||
                        Get_Numeric(Get_Date(this.DTP01_CHYSDATE.GetValue().ToString().Trim())) != Get_Numeric(fsCHYSDATE.ToString().Trim()) ||
                        Get_Numeric(this.TXT01_CHYDSEQ.GetValue().ToString()) != Get_Numeric(fsCHYDSEQ.ToString().Trim()) ||
                        Get_Numeric(this.TXT01_CHYSSEQ.GetValue().ToString()) != Get_Numeric(fsCHYSSEQ.ToString().Trim()) ||
                        this.TXT01_CHCHTANK.GetValue().ToString().Trim() != fsCHCHTANK.ToString().Trim()
                        )
                    {
                        this.ShowMessage("TY_M_UT_6CLFS145");
                        this.TXT01_CHJGHWAJU.Focus();

                        e.Successed = false;
                        return;
                    }
                }

                // 출고 - SURVEY 체크
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_UT_6CLG1146",
                    Get_Date(this.DTP01_CHIPHANG.GetValue().ToString().Trim()),
                    this.CBH01_CHBONSUN.GetValue().ToString().Trim(),
                    this.CBH01_CHHWAJU.GetValue().ToString().Trim(),
                    this.CBH01_CHHWAMUL.GetValue().ToString().Trim(),
                    this.TXT01_CHCHTANK.GetValue().ToString().Trim()
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    fsSVMTQTY = dt.Rows[0]["SVMTQTY"].ToString();
                    fsSVCHQTY = dt.Rows[0]["SVCHULQTY"].ToString();
                    fsSVKLQTY = dt.Rows[0]["SVKLQTY"].ToString();
                    fsSVBIJUNG = dt.Rows[0]["SVBIJUNG"].ToString();
                }
                else
                {
                    this.ShowMessage("TY_M_UT_6AKCN443");
                    this.TXT01_CHJGHWAJU.Focus();

                    e.Successed = false;
                    return;
                }

                if (double.Parse(Get_Numeric(this.TXT01_CHKLQTY.GetValue().ToString().Trim())) == 0)
                {
                    if (double.Parse(Get_Numeric(fsMTQTY.ToString())) == 0 || double.Parse(Get_Numeric(fsSVKLQTY.ToString())) == 0)
                    {
                        fsKLQTY =
                            (
                            double.Parse(String.Format("{0,9:N3}", Get_Numeric(this.TXT01_CHMTQTY.GetValue().ToString().Trim())))
                            / double.Parse(Get_Numeric(fsSVBIJUNG.ToString()))
                            ).ToString("0.000");
                    }
                    else
                    {
                        fsKLQTY =
                            (
                              double.Parse(String.Format("{0,9:N3}", Get_Numeric(this.TXT01_CHMTQTY.GetValue().ToString().Trim())))
                            / double.Parse(String.Format("{0,9:N3}", Get_Numeric(fsMTQTY.ToString())))
                            * double.Parse(String.Format("{0,9:N3}", Get_Numeric(fsSVKLQTY.ToString())))
                            ).ToString("0.000");
                    }
                }

                fsDJCHQTY = "0"; // DRUM 출고수량
                fsDJPOQTY = "0"; // DRUM 포장수량
                fsDJJEQTY = "0"; // DRUM 포장재고

                // 출고 - DRUM 재고 체크
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_UT_6CLGO147",
                    this.CBH01_CHACTHJ.GetValue().ToString().Trim(),
                    this.CBH01_CHHWAMUL.GetValue().ToString().Trim(),
                    Get_Date(this.DTP01_CHIPHANG.GetValue().ToString().Trim()),
                    this.CBH01_CHBONSUN.GetValue().ToString().Trim(),
                    this.CBH01_CHHWAJU.GetValue().ToString().Trim(),
                    this.TXT01_CHBLNO.GetValue().ToString().Trim(),
                    Get_Numeric(this.TXT01_CHMSNSEQ.GetValue().ToString().Trim()),
                    Get_Numeric(this.TXT01_CHHSNSEQ.GetValue().ToString().Trim()),
                    Get_Date(this.DTP01_CHCUSTIL.GetValue().ToString().Trim()),
                    Get_Numeric(this.TXT01_CHCHASU.GetValue().ToString().Trim()),
                    this.TXT01_CHJGHWAJU.GetValue().ToString(),
                    this.CBH01_CHYSHWAJU.GetValue().ToString(),
                    this.CBH01_CHYDHWAJU.GetValue().ToString(),
                    Get_Date(this.DTP01_CHYSDATE.GetValue().ToString().Trim()),
                    this.TXT01_CHYDSEQ.GetValue().ToString(),
                    this.TXT01_CHYSSEQ.GetValue().ToString(),
                    Get_Numeric(this.TXT01_CHJUNG.GetValue().ToString().Trim())
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    fsDJCHQTY = dt.Rows[0]["DJCHQTY"].ToString(); // DRUM 출고수량
                    fsDJPOQTY = dt.Rows[0]["DJPOQTY"].ToString(); // DRUM 포장수량
                    fsDJJEQTY = dt.Rows[0]["DJJEQTY"].ToString(); // DRUM 포장재고
                }
                else
                {
                    this.ShowMessage("TY_M_UT_6CLGY148");
                    this.TXT01_CHQTY.Focus();

                    e.Successed = false;
                    return;
                }

                if (fsGUBUN.ToString() == "NEW")
                {
                    if (double.Parse(String.Format("{0,9:N3}", Get_Numeric(fsDJCHQTY.ToString()))) + double.Parse(String.Format("{0,9:N3}", Get_Numeric(this.TXT01_CHQTY.GetValue().ToString().Trim())))
                        > double.Parse(String.Format("{0,9:N3}", Get_Numeric(fsDJPOQTY.ToString())))
                       )
                    {
                        this.ShowMessage("TY_M_UT_6CLGZ149");
                        this.TXT01_CHQTY.Focus();

                        e.Successed = false;
                        return;
                    }
                }
                else if (fsGUBUN.ToString() == "UPT")
                {
                    if (double.Parse(String.Format("{0,9:N3}", Get_Numeric(fsDJCHQTY.ToString())))
                          - double.Parse(String.Format("{0,9:N3}", Get_Numeric(fsCHQTY.ToString())))
                          + double.Parse(String.Format("{0,9:N3}", Get_Numeric(this.TXT01_CHQTY.GetValue().ToString().Trim())))
                        > double.Parse(String.Format("{0,9:N3}", Get_Numeric(fsDJPOQTY.ToString())))
                       )
                    {
                        this.ShowMessage("TY_M_UT_6CLGZ149");
                        this.TXT01_CHQTY.Focus();

                        e.Successed = false;
                        return;
                    }
                }

                // 출고 - 탱크별 DRUM 재고 체크
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_UT_6CLH8151",
                    this.CBH01_CHACTHJ.GetValue().ToString().Trim(),
                    this.CBH01_CHHWAMUL.GetValue().ToString().Trim(),
                    Get_Date(this.DTP01_CHIPHANG.GetValue().ToString().Trim()),
                    this.CBH01_CHBONSUN.GetValue().ToString().Trim(),
                    this.CBH01_CHHWAJU.GetValue().ToString().Trim(),
                    this.TXT01_CHCHTANK.GetValue().ToString().Trim(),
                    Get_Numeric(this.TXT01_CHJUNG.GetValue().ToString().Trim())
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    fsDTCHQTY = dt.Rows[0]["DTCHQTY"].ToString(); // DRUM 출고수량
                    fsDTPOQTY = dt.Rows[0]["DTPOQTY"].ToString(); // DRUM 포장수량
                    fsDTJEQTY = dt.Rows[0]["DTJEQTY"].ToString(); // DRUM 포장재고
                }
                else
                {
                    this.ShowMessage("TY_M_UT_6CLH0152");
                    this.TXT01_CHQTY.Focus();

                    e.Successed = false;
                    return;
                }


                if (fsGUBUN.ToString() == "NEW")
                {
                    if (double.Parse(String.Format("{0,9:N3}", Get_Numeric(fsDTCHQTY.ToString())))
                        + double.Parse(String.Format("{0,9:N3}", Get_Numeric(this.TXT01_CHQTY.GetValue().ToString().Trim())))
                        > double.Parse(String.Format("{0,9:N3}", Get_Numeric(fsDTPOQTY.ToString())))
                       )
                    {
                        this.ShowMessage("TY_M_UT_6CLH4153");
                        this.TXT01_CHQTY.Focus();

                        e.Successed = false;
                        return;
                    }
                }
                else if (fsGUBUN.ToString() == "UPT")
                {
                    if (this.TXT01_CHCHTANK.GetValue().ToString().Trim() == fsCHCHTANK.ToString().Trim())
                    {
                        if (double.Parse(String.Format("{0,9:N3}", Get_Numeric(fsDTCHQTY.ToString())))
                              - double.Parse(String.Format("{0,9:N3}", Get_Numeric(fsCHQTY.ToString())))
                              + double.Parse(String.Format("{0,9:N3}", Get_Numeric(this.TXT01_CHQTY.GetValue().ToString().Trim())))
                            > double.Parse(String.Format("{0,9:N3}", Get_Numeric(fsDTPOQTY.ToString())))
                           )
                        {
                            this.ShowMessage("TY_M_UT_6CLH4153");
                            this.TXT01_CHQTY.Focus();

                            e.Successed = false;
                            return;
                        }
                    }
                    else
                    {
                        if (double.Parse(String.Format("{0,9:N3}", Get_Numeric(fsDTCHQTY.ToString())))
                            + double.Parse(String.Format("{0,9:N3}", Get_Numeric(this.TXT01_CHQTY.GetValue().ToString().Trim())))
                            > double.Parse(String.Format("{0,9:N3}", Get_Numeric(fsDTPOQTY.ToString())))
                           )
                        {
                            this.ShowMessage("TY_M_UT_6CLH4153");
                            this.TXT01_CHQTY.Focus();

                            e.Successed = false;
                            return;
                        }
                    }
                }

                if (fsGUBUN.ToString() == "UPT")
                {
                    // 일자별 DRUM 출고 파일 체크
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_UT_6CLHF155",
                        Get_Date(this.DTP01_CHCHULIL.GetValue().ToString().Trim()),
                        this.CBH01_CHACTHJ.GetValue().ToString().Trim(),
                        this.CBH01_CHHWAMUL.GetValue().ToString().Trim(),
                        Get_Date(this.DTP01_CHIPHANG.GetValue().ToString().Trim()),
                        this.CBH01_CHBONSUN.GetValue().ToString().Trim(),
                        this.CBH01_CHHWAJU.GetValue().ToString().Trim(),
                        this.TXT01_CHBLNO.GetValue().ToString().Trim(),
                        Get_Numeric(this.TXT01_CHMSNSEQ.GetValue().ToString().Trim()),
                        Get_Numeric(this.TXT01_CHHSNSEQ.GetValue().ToString().Trim()),
                        Get_Date(this.DTP01_CHCUSTIL.GetValue().ToString().Trim()),
                        Get_Numeric(this.TXT01_CHCHASU.GetValue().ToString().Trim()),
                        this.TXT01_CHJGHWAJU.GetValue().ToString(),
                        this.CBH01_CHYSHWAJU.GetValue().ToString(),
                        this.CBH01_CHYDHWAJU.GetValue().ToString(),
                        Get_Date(this.DTP01_CHYSDATE.GetValue().ToString().Trim()),
                        this.TXT01_CHYDSEQ.GetValue().ToString(),
                        this.TXT01_CHYSSEQ.GetValue().ToString(),
                        Get_Numeric(this.TXT01_CHJUNG.GetValue().ToString().Trim())
                        );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count <= 0)
                    {
                        this.ShowMessage("TY_M_UT_6CLHG156");
                        this.TXT01_CHQTY.Focus();

                        e.Successed = false;
                        return;
                    }
                }
            }

            #endregion

            if (fsGUBUN == "UPT")
            {
                //this.DbConnector.CommandClear();
                //this.DbConnector.Attach
                //    (
                //    "TY_P_UT_6CLD4142",
                //    Get_Date(this.DTP01_CHCHULIL.GetValue().ToString()), // 출고일자
                //    this.TXT01_CHTKNO.GetValue().ToString()
                //    );

                //dt = this.DbConnector.ExecuteDataTable();

                //if (dt.Rows.Count > 0)
                //{
                //    fsCHIPHANG  = dt.Rows[0]["CHIPHANG"].ToString();
                //    fsCHBONSUN  = dt.Rows[0]["CHBONSUN"].ToString();
                //    fsCHHWAJU   = dt.Rows[0]["CHHWAJU"].ToString();
                //    fsCHHWAMUL  = dt.Rows[0]["CHHWAMUL"].ToString();
                //    fsCHBLNO    = dt.Rows[0]["CHBLNO"].ToString();
                //    fsCHMSNSEQ  = dt.Rows[0]["CHMSNSEQ"].ToString();
                //    fsCHHSNSEQ  = dt.Rows[0]["CHHSNSEQ"].ToString();
                //    fsCHACTHJ   = dt.Rows[0]["CHACTHJ"].ToString();
                //    fsCHCUSTIL  = dt.Rows[0]["CHCUSTIL"].ToString();
                //    fsCHCHASU   = dt.Rows[0]["CHCHASU"].ToString();

                //    fsCHJGHWAJU = dt.Rows[0]["CHJGHWAJU"].ToString();
                //    fsCHYSHWAJU = dt.Rows[0]["CHYSHWAJU"].ToString();
                //    fsCHYDHWAJU = dt.Rows[0]["CHYDHWAJU"].ToString();
                //    fsCHYSDATE  = dt.Rows[0]["CHYSDATE"].ToString();
                //    fsCHYDSEQ   = dt.Rows[0]["CHYDSEQ"].ToString();
                //    fsCHYSSEQ   = dt.Rows[0]["CHYSSEQ"].ToString();

                //    fsCHCHTANK  = dt.Rows[0]["CHCHTANK"].ToString();
                //    fsCHIPTANK  = dt.Rows[0]["CHIPTANK"].ToString();
                //    fsCHCHULGB  = dt.Rows[0]["CHCHULGB"].ToString();
                //    fsCHMTQTY   = dt.Rows[0]["CHMTQTY"].ToString();
                //    fsCHYNCHQTY_AGO = dt.Rows[0]["CHYNCHQTY"].ToString();
                //    fsCHKLQTY   = dt.Rows[0]["CHKLQTY"].ToString();
                //    fsCHDANYI   = dt.Rows[0]["CHDANYI"].ToString();
                //    fsCHJUNG    = dt.Rows[0]["CHJUNG"].ToString();
                //    fsCHDRQTY   = dt.Rows[0]["CHQTY"].ToString();
                //    fsCHJUNG    = dt.Rows[0]["CHJUNG"].ToString();
                //    fsCHCHHJ    = dt.Rows[0]["CHCHHJ"].ToString();
                //    fsCHHWAPE   = dt.Rows[0]["CHHWAPE"].ToString();
                //    fsCHJISINUM = dt.Rows[0]["CHJISINUM"].ToString();
                //    fsCHHIGB    = dt.Rows[0]["CHHIGB"].ToString();
                //    fsCHCHTANK  = dt.Rows[0]["CHCHTANK"].ToString();
                //    fsCHIPTANK  = dt.Rows[0]["CHIPTANK"].ToString();
                //}

                // 출고 - SURVEY 체크
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_UT_6CLG1146",
                    fsCHIPHANG.ToString().Trim(),
                    fsCHBONSUN.ToString().Trim(),
                    fsCHHWAJU.ToString().Trim(),
                    fsCHHWAMUL.ToString().Trim(),
                    fsCHCHTANK.ToString().Trim()
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    fsMTQTY = dt.Rows[0]["SVMTQTY"].ToString();
                    fsCHQTY = dt.Rows[0]["SVCHULQTY"].ToString();
                    fsSVKLQTY = dt.Rows[0]["SVKLQTY"].ToString();
                    fsSVBIJUNG = dt.Rows[0]["SVBIJUNG"].ToString();
                }
                else
                {
                    fsMTQTY = "0";
                    fsCHQTY = "0";
                }

                fsSCCHJEQTY = (
                                 double.Parse(String.Format("{0,9:N3}", fsMTQTY.ToString()))
                               - double.Parse(String.Format("{0,9:N3}", fsCHQTY.ToString()))
                              ).ToString("0.000");

                // 매출입고 할증파일
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_UT_6CMBO163",
                    fsCHIPHANG.ToString().Trim(),
                    fsCHBONSUN.ToString().Trim(),
                    fsCHHWAJU.ToString().Trim(),
                    fsCHHWAMUL.ToString().Trim(),
                    fsCHIPTANK.ToString().Trim()
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count <= 0)
                {
                    fsMTQTY = "0";
                    fsCHQTY = "0";
                }
                else
                {
                    fsMTQTY = Get_Numeric(dt.Rows[0]["COMTQTY"].ToString());
                    fsCHQTY = Get_Numeric(dt.Rows[0]["COCHQTY"].ToString());
                }



                fsSCIPJEQTY = (
                                 double.Parse(String.Format("{0,9:N3}", fsMTQTY.ToString()))
                               - double.Parse(String.Format("{0,9:N3}", fsCHQTY.ToString()))
                              ).ToString("0.000");
            }

            #region Desription : 할증료 계산

            string sCOCONTNO = string.Empty;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_6CMBO163",
                Get_Date(this.DTP01_CHIPHANG.GetValue().ToString()),
                this.CBH01_CHBONSUN.GetValue().ToString().Trim(),
                this.CBH01_CHHWAJU.GetValue().ToString().Trim(),
                this.CBH01_CHHWAMUL.GetValue().ToString().Trim(),
                this.TXT01_CHIPTANK.GetValue().ToString().Trim()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count <= 0)
            {
                this.ShowMessage("TY_M_UT_6AKCO445");
                SetFocus(this.TXT01_CHJGHWAJU);

                e.Successed = false;
                return;
            }
            else
            {
                sCOCONTNO = dt.Rows[0]["COCONTNO"].ToString();
            }

            string sCNHANDAM = string.Empty;
            string sCNHANDDA = string.Empty;
            string sCNHANDHP = string.Empty;
            string sCNHANDOV = string.Empty;
            string sCNISDA = string.Empty;
            string sCNISAM = string.Empty;
            string sCNISHP = string.Empty;
            string sCNIPDA = string.Empty;
            string sCNIPAM = string.Empty;
            string sCNIPHP = string.Empty;
            string sCNCHDA = string.Empty;
            string sCNCHAM = string.Empty;
            string sCNCHHP = string.Empty;
            double dKESAN = 0;

            // 출고 - SURVEY 체크
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_6CLG1146",
                Get_Date(this.DTP01_CHIPHANG.GetValue().ToString().Trim()),
                this.CBH01_CHBONSUN.GetValue().ToString().Trim(),
                this.CBH01_CHHWAJU.GetValue().ToString().Trim(),
                this.CBH01_CHHWAMUL.GetValue().ToString().Trim(),
                this.TXT01_CHCHTANK.GetValue().ToString().Trim()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                fsSVMTQTY = dt.Rows[0]["SVMTQTY"].ToString();
                fsSVCHQTY = dt.Rows[0]["SVCHULQTY"].ToString();
                fsSVKLQTY = dt.Rows[0]["SVKLQTY"].ToString();
                fsSVBIJUNG = dt.Rows[0]["SVBIJUNG"].ToString();
            }
            else
            {
                this.ShowMessage("TY_M_UT_6AKCN443");
                this.TXT01_CHJGHWAJU.Focus();

                e.Successed = false;
                return;
            }

            //if (Get_Numeric(this.TXT01_CHKLQTY.GetValue().ToString().Trim()) == "0")
            //{
            if (double.Parse(Get_Numeric(fsSVMTQTY.ToString())) == 0 || double.Parse(Get_Numeric(fsSVKLQTY.ToString())) == 0)
            {
                fsKLQTY =
                    (
                       double.Parse(String.Format("{0,9:N3}", Get_Numeric(this.TXT01_CHMTQTY.GetValue().ToString().Trim())))
                     / double.Parse(String.Format("{0,9:N3}", Get_Numeric(fsSVBIJUNG.ToString())))
                    ).ToString("0.000");
            }
            else
            {
                fsKLQTY =
                    (
                       double.Parse(String.Format("{0,9:N3}", Get_Numeric(this.TXT01_CHMTQTY.GetValue().ToString().Trim())))
                     / double.Parse(String.Format("{0,9:N3}", Get_Numeric(fsSVMTQTY.ToString())))
                     * double.Parse(String.Format("{0,9:N3}", Get_Numeric(fsSVKLQTY.ToString())))
                    ).ToString("0.000");
            }

            this.TXT01_CHKLQTY.SetValue(Get_Numeric(fsKLQTY.ToString()));
            //}

            // 계약번호 체크
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_6CMDI168", sCOCONTNO.ToString());

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count <= 0)
            {
                this.ShowMessage("TY_M_UT_6CMGS176");
                SetFocus(this.TXT01_CHJGHWAJU);

                e.Successed = false;
                return;
            }
            else
            {
                sCNHANDAM = dt.Rows[0]["CNHANDAM"].ToString();
                sCNHANDDA = dt.Rows[0]["CNHANDDA"].ToString();
                sCNHANDHP = dt.Rows[0]["CNHANDHP"].ToString();
                sCNHANDOV = dt.Rows[0]["CNHANDOV"].ToString();
                sCNISDA = dt.Rows[0]["CNISDA"].ToString();
                sCNISAM = dt.Rows[0]["CNISAM"].ToString();
                sCNISHP = dt.Rows[0]["CNISHP"].ToString();
                sCNIPDA = dt.Rows[0]["CNIPDA"].ToString();
                sCNIPAM = dt.Rows[0]["CNIPAM"].ToString();
                sCNIPHP = dt.Rows[0]["CNIPHP"].ToString();
                sCNCHDA = dt.Rows[0]["CNCHDA"].ToString();
                sCNCHAM = dt.Rows[0]["CNCHAM"].ToString();
                sCNCHHP = dt.Rows[0]["CNCHHP"].ToString();

                fsCONVERT =
                        (
                          double.Parse(String.Format("{0,9:N3}", Get_Numeric(this.TXT01_CHOVQTY.GetValue().ToString())))
                        / double.Parse(String.Format("{0,9:N3}", Get_Numeric(this.TXT01_CHMTQTY.GetValue().ToString())))
                        * double.Parse(String.Format("{0,9:N3}", Get_Numeric(this.TXT01_CHKLQTY.GetValue().ToString())))
                        ).ToString("0.0000000");

                double dCONVERT = double.Parse(Get_Numeric(fsCONVERT.ToString()));
                fsOVER_KL = dCONVERT.ToString("#0.000");


                if (Get_Numeric(sCNHANDAM.ToString()) != "0")
                {
                    if (sCNHANDDA.ToString() == "MT")
                    {
                        fsKESAN =
                            (
                               double.Parse(String.Format("{0,9:N3}", Get_Numeric(this.TXT01_CHOVQTY.GetValue().ToString())))
                             * double.Parse(String.Format("{0,9:N2}", Get_Numeric(sCNHANDAM.ToString())))
                            ).ToString("0.0000000");

                        dKESAN = double.Parse(Get_Numeric(fsKESAN.ToString()));
                        fsKESAN = Convert.ToString(dKESAN);
                    }
                    else
                    {
                        fsKESAN =
                            (
                               double.Parse(String.Format("{0,9:N3}", fsOVER_KL.ToString()))
                             * double.Parse(String.Format("{0,9:N2}", Get_Numeric(sCNHANDAM.ToString())))
                            ).ToString("0.0000000");

                        dKESAN = double.Parse(Get_Numeric(fsKESAN.ToString()));
                        fsKESAN = Convert.ToString(dKESAN);
                    }
                    fsCHHWAPE = sCNHANDHP.ToString();
                }
                else
                {
                    if (this.CBH01_CHCHULGB.GetValue().ToString() == "05")
                    {
                        if (sCNISDA.ToString() == "MT")
                        {
                            fsKESAN =
                                (
                                  double.Parse(String.Format("{0,9:N3}", Get_Numeric(this.TXT01_CHOVQTY.GetValue().ToString())))
                                * double.Parse(String.Format("{0,9:N2}", Get_Numeric(sCNISAM.ToString())))
                                * double.Parse(String.Format("{0,9:N0}", Get_Numeric(sCNHANDOV.ToString()))) / 100
                                ).ToString("0.0000000");

                            dKESAN = double.Parse(Get_Numeric(fsKESAN.ToString()));
                            fsKESAN = Convert.ToString(dKESAN);
                            fsCHHWAPE = sCNISHP.ToString();
                        }
                        else
                        {
                            fsKESAN =
                                (
                                  double.Parse(String.Format("{0,9:N3}", fsOVER_KL.ToString()))
                                * double.Parse(String.Format("{0,9:N2}", Get_Numeric(sCNISAM.ToString())))
                                * double.Parse(String.Format("{0,9:N0}", Get_Numeric(sCNHANDOV.ToString()))) / 100
                                ).ToString("0.0000000");

                            dKESAN = double.Parse(Get_Numeric(fsKESAN.ToString()));
                            fsKESAN = Convert.ToString(dKESAN);
                            fsCHHWAPE = sCNISHP.ToString();
                        }
                    }
                    else if (Get_Numeric(sCNIPAM.ToString()) != "0" && Get_Numeric(sCNCHAM.ToString()) != "0")
                    {
                        if (sCNCHDA.ToString() == "MT")
                        {
                            fsKESAN =
                                (
                                  double.Parse(String.Format("{0,9:N3}", Get_Numeric(this.TXT01_CHOVQTY.GetValue().ToString())))
                                * double.Parse(String.Format("{0,9:N2}", Get_Numeric(sCNCHAM.ToString())))
                                * double.Parse(String.Format("{0,9:N0}", Get_Numeric(sCNHANDOV.ToString()))) / 100
                                ).ToString("0.0000000");

                            dKESAN = double.Parse(Get_Numeric(fsKESAN.ToString()));
                            fsKESAN = Convert.ToString(dKESAN);
                            fsCHHWAPE = sCNCHHP.ToString();
                        }
                        else
                        {
                            fsKESAN =
                                (
                                  double.Parse(String.Format("{0,9:N3}", fsOVER_KL.ToString()))
                                * double.Parse(String.Format("{0,9:N2}", Get_Numeric(sCNCHAM.ToString())))
                                * double.Parse(String.Format("{0,9:N0}", Get_Numeric(sCNHANDOV.ToString()))) / 100
                                ).ToString("0.0000000");

                            dKESAN = double.Parse(Get_Numeric(fsKESAN.ToString()));
                            fsKESAN = Convert.ToString(dKESAN);
                            fsCHHWAPE = sCNCHHP.ToString();
                        }
                    }
                    else if (Get_Numeric(sCNCHAM.ToString()) == "0")
                    {
                        if (sCNIPDA.ToString() == "MT")
                        {
                            fsKESAN =
                                (
                                  double.Parse(String.Format("{0,9:N3}", Get_Numeric(this.TXT01_CHOVQTY.GetValue().ToString())))
                                * double.Parse(String.Format("{0,9:N2}", Get_Numeric(sCNIPAM.ToString())))
                                * double.Parse(String.Format("{0,9:N0}", Get_Numeric(sCNHANDOV.ToString()))) / 100
                                ).ToString("0.0000000");

                            dKESAN = double.Parse(Get_Numeric(fsKESAN.ToString()));
                            fsKESAN = Convert.ToString(dKESAN);
                            fsCHHWAPE = sCNIPHP.ToString();
                        }
                        else
                        {
                            fsKESAN =
                                (
                                  double.Parse(String.Format("{0,9:N3}", fsOVER_KL.ToString()))
                                * double.Parse(String.Format("{0,9:N2}", Get_Numeric(sCNIPAM.ToString())))
                                * double.Parse(String.Format("{0,9:N0}", Get_Numeric(sCNHANDOV.ToString()))) / 100
                                ).ToString("0.0000000");

                            dKESAN = double.Parse(Get_Numeric(fsKESAN.ToString()));
                            fsKESAN = Convert.ToString(dKESAN);
                            fsCHHWAPE = sCNIPHP.ToString();
                        }
                    }
                    else
                    {
                        if (sCNCHDA.ToString() == "MT")
                        {
                            fsKESAN =
                                (
                                  double.Parse(String.Format("{0,9:N3}", Get_Numeric(this.TXT01_CHOVQTY.GetValue().ToString())))
                                * double.Parse(String.Format("{0,9:N2}", Get_Numeric(sCNCHAM.ToString())))
                                * double.Parse(String.Format("{0,9:N0}", Get_Numeric(sCNHANDOV.ToString()))) / 100
                                ).ToString("0.0000000");

                            dKESAN = double.Parse(Get_Numeric(fsKESAN.ToString()));
                            fsKESAN = Convert.ToString(dKESAN);
                            fsCHHWAPE = sCNCHHP.ToString();
                        }
                        else
                        {
                            fsKESAN =
                                (
                                  double.Parse(String.Format("{0,9:N3}", fsOVER_KL.ToString()))
                                * double.Parse(String.Format("{0,9:N2}", Get_Numeric(sCNCHAM.ToString())))
                                * double.Parse(String.Format("{0,9:N0}", Get_Numeric(sCNHANDOV.ToString()))) / 100
                                ).ToString("0.0000000");

                            dKESAN = double.Parse(Get_Numeric(fsKESAN.ToString()));
                            fsKESAN = Convert.ToString(dKESAN);
                            fsCHHWAPE = sCNCHHP.ToString();
                        }
                    }
                }
                if (fsCHHWAPE.ToString() != "" && fsCHHWAPE.ToString() != "2")
                {
                    dKESAN = double.Parse(Get_Numeric(fsKESAN.ToString()));
                    fsKESAN = (dKESAN).ToString("#0");
                    this.TXT01_CHOVAM.SetValue(fsKESAN.ToString());
                }
                else
                {
                    dKESAN = double.Parse(Get_Numeric(fsKESAN.ToString()));
                    fsKESAN = (dKESAN).ToString("#0");
                    this.TXT01_CHOVAM.SetValue(fsKESAN.ToString());
                }
            }

            #endregion



            if (int.Parse(Get_Date(this.DTP01_CHCHULIL.GetValue().ToString())) < int.Parse(Get_Date(this.DTP01_CHIPHANG.GetValue().ToString())))
            {
                this.ShowMessage("TY_M_UT_6CMAX158");
                this.DTP01_CHCHULIL.Focus();

                e.Successed = false;
                return;
            }

            if (Get_Numeric(Get_Date(this.DTP01_CHYSDATE.GetValue().ToString())) != "0")
            {
                if (int.Parse(Get_Date(this.DTP01_CHCHULIL.GetValue().ToString())) < int.Parse(Get_Date(this.DTP01_CHYSDATE.GetValue().ToString())))
                {
                    this.ShowMessage("TY_M_UT_6CSBV249");
                    this.DTP01_CHCHULIL.Focus();

                    e.Successed = false;
                    return;
                }
            }

            if (fsGUBUN.ToString() == "UPT")
            {
                if (fsCHDANYI.ToString() != this.CBO01_CHDANYI.GetValue().ToString())
                {
                    this.ShowMessage("TY_M_UT_6CMBB160");
                    this.CBO01_CHDANYI.Focus();

                    e.Successed = false;
                    return;
                }
            }

            if (fsGUBUN.ToString() == "NEW")
            {
                string sCHCHULGB = string.Empty;

                if (this.CBH01_CHCHULGB.GetValue().ToString() == "01")
                {
                    sCHCHULGB = "01";
                }
                else
                {
                    if (this.CBH01_CHCHULGB.GetValue().ToString() == "02" || this.CBH01_CHCHULGB.GetValue().ToString() == "09" || this.CBH01_CHCHULGB.GetValue().ToString() == "10")
                    {
                        sCHCHULGB = "02";
                    }
                    else
                    {
                        sCHCHULGB = this.CBH01_CHCHULGB.GetValue().ToString();
                    }
                }

                // 출고순번 가져오기
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_UT_6CMB2159",
                    sCHCHULGB.ToString(),
                    sCHCHULGB.ToString(),
                    sCHCHULGB.ToString(),
                    sCHCHULGB.ToString(),
                    sCHCHULGB.ToString(),
                    Get_Date(this.DTP01_CHCHULIL.GetValue().ToString()),
                    sCHCHULGB.ToString()
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.TXT01_CHTKNO.SetValue(dt.Rows[0]["CATKNO"].ToString());
                }

                // 20180817 김종술 과장 요청
                //int iCHTKNO = 0;
                //iCHTKNO = int.Parse(this.TXT01_CHTKNO.GetValue().ToString());

                //if (iCHTKNO >= 500 && iCHTKNO <= 599)
                //{
                //    this.CBH01_CHCHULGB.SetValue("05");
                //    this.CBH01_CHCHJANG.SetValue("1");
                //    this.CBO01_CHDANYI.SetValue("4");
                //}

                //if (iCHTKNO >= 800 && iCHTKNO <= 899)
                //{
                //    this.CBH01_CHCHULGB.SetValue("03");
                //    this.CBH01_CHCHJANG.SetValue("4");
                //    this.CBO01_CHDANYI.SetValue("1");
                //}

                //if (iCHTKNO > 900)
                //{
                //    this.CBH01_CHCHULGB.SetValue("04");
                //    this.CBH01_CHCHJANG.SetValue("4");
                //    this.CBO01_CHDANYI.SetValue("1");
                //}

                int iCHCHTANK = int.Parse(this.TXT01_CHCHTANK.GetValue().ToString().Trim());

                if (iCHCHTANK > 5000)
                {
                    //해안단지
                    this.CBH01_CHCHJANG.SetValue("7");
                }
                else if (iCHCHTANK > 3000)
                {
                    //상단지
                    this.CBH01_CHCHJANG.SetValue("1");
                }
                else if (iCHCHTANK > 2000)
                {
                    //송유단지
                    this.CBH01_CHCHJANG.SetValue("5");
                    this.CBO01_CHDANYI.SetValue("1");
                }
                else if (iCHCHTANK > 500 && iCHCHTANK <= 1199)
                {
                    //상단지
                    this.CBH01_CHCHJANG.SetValue("1");
                }
                else if (iCHCHTANK > 100 && iCHCHTANK <= 399)
                {
                    //하단지
                    this.CBH01_CHCHJANG.SetValue("2");
                }

                if (this.CBH01_CHCHULGB.GetValue().ToString() == "03" || this.CBH01_CHCHULGB.GetValue().ToString() == "04")
                {
                    // 출하구분이 선적 또는 SHORTAGE 인 경우 출하장소 부두
                    this.CBH01_CHCHJANG.SetValue("4");
                }
            }


            if (fsGUBUN == "UPT")
            {
                if (Get_Date(this.DTP01_CHIPHANG.GetValue().ToString().Trim()) != fsCHIPHANG.ToString().Trim() ||
                    this.CBH01_CHBONSUN.GetValue().ToString().Trim().ToUpper() != fsCHBONSUN.ToString().Trim() ||
                    this.CBH01_CHHWAJU.GetValue().ToString().Trim().ToUpper() != fsCHHWAJU.ToString().Trim() ||
                    this.CBH01_CHHWAMUL.GetValue().ToString().Trim().ToUpper() != fsCHHWAMUL.ToString().Trim() ||
                    this.TXT01_CHBLNO.GetValue().ToString().Trim() != fsCHBLNO.ToString().Trim() ||
                    Get_Numeric(this.TXT01_CHMSNSEQ.GetValue().ToString().Trim()) != fsCHMSNSEQ.ToString().Trim() ||
                    Get_Numeric(this.TXT01_CHHSNSEQ.GetValue().ToString().Trim()) != fsCHHSNSEQ.ToString().Trim() ||
                    this.CBH01_CHACTHJ.GetValue().ToString() != fsCHACTHJ.ToString().Trim() ||
                    Get_Date(this.DTP01_CHCUSTIL.GetValue().ToString().Trim()) != fsCHCUSTIL.ToString().Trim() ||
                    Get_Numeric(this.TXT01_CHCHASU.GetValue().ToString().Trim()) != fsCHCHASU.ToString().Trim() ||
                    this.TXT01_CHJGHWAJU.GetValue().ToString() != fsCHJGHWAJU.ToString().Trim() ||
                    this.CBH01_CHYSHWAJU.GetValue().ToString() != fsCHYSHWAJU.ToString().Trim() ||
                    this.CBH01_CHYDHWAJU.GetValue().ToString() != fsCHYDHWAJU.ToString().Trim() ||
                    Get_Numeric(Get_Date(this.DTP01_CHYSDATE.GetValue().ToString().Trim())) != Get_Numeric(fsCHYSDATE.ToString().Trim()) ||
                    Get_Numeric(this.TXT01_CHYDSEQ.GetValue().ToString()) != Get_Numeric(fsCHYDSEQ.ToString().Trim()) ||
                    Get_Numeric(this.TXT01_CHYSSEQ.GetValue().ToString()) != Get_Numeric(fsCHYSSEQ.ToString().Trim()) ||
                    this.TXT01_CHCHTANK.GetValue().ToString().Trim() != fsCHCHTANK.ToString().Trim() ||
                    this.TXT01_CHIPTANK.GetValue().ToString().Trim() != fsCHIPTANK.ToString().Trim() ||
                    Get_Numeric(this.TXT01_CHJUNG.GetValue().ToString()) != Get_Numeric(fsCHJUNG.ToString())
                    )
                {
                    this.ShowMessage("TY_M_UT_6CLFS145");
                    this.TXT01_CHJGHWAJU.Focus();

                    e.Successed = false;
                    return;
                }
            }






            if (int.Parse(Get_Date(this.DTP01_CHCHULIL.GetValue().ToString())) < int.Parse(Get_Date(this.DTP01_CHCUSTIL.GetValue().ToString())))
            {
                this.ShowMessage("TY_M_UT_6CMBK161");
                this.TXT01_CHJGHWAJU.Focus();

                e.Successed = false;
                return;
            }



            // 출하구분 송유, 선적일 경우 패스 시킴.
            if (this.CBH01_CHCHULGB.GetValue().ToString().Trim() != "04" && this.CBH01_CHCHULGB.GetValue().ToString().Trim() != "05")
            {
                // 20180112 고지파트 요청
                // 출하자 및 외부출하 모두 입력이 되어도 저장 및 수정이 가능토록 요청
                //if (this.CBH01_CHCHHASAB.GetValue().ToString().Trim() != "" && this.CBH01_CHOUTCHSAB.GetValue().ToString().Trim() != "")
                //{
                //    this.ShowMessage("TY_M_UT_6CMBV166");
                //    this.TXT01_CHJGHWAJU.Focus();

                //    e.Successed = false;
                //    return;
                //}

                if (this.CBH01_CHCHHASAB.GetValue().ToString().Trim() == "" && this.CBH01_CHOUTCHSAB.GetValue().ToString().Trim() == "")
                {
                    this.ShowMessage("TY_M_UT_6CMBW167");

                    SetFocus(this.CBH01_CHCHHASAB.CodeText);

                    e.Successed = false;
                    return;
                }
            }

            // 출고 탱크번호 체크
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_66SDH426", this.TXT01_CHCHTANK.GetValue().ToString().Trim());

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count <= 0)
            {
                this.ShowMessage("TY_M_UT_6CMBL162");
                SetFocus(this.TXT01_CHIPTANK);

                e.Successed = false;
                return;
            }
            else
            {
                fsCHBIJUNG = dt.Rows[0]["TNBIJUNG"].ToString();
                fsCHVCF = dt.Rows[0]["TNVCF"].ToString();
            }

            // 출고 - SURVEY 체크
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_6CLG1146",
                Get_Date(this.DTP01_CHIPHANG.GetValue().ToString().Trim()),
                this.CBH01_CHBONSUN.GetValue().ToString().Trim(),
                this.CBH01_CHHWAJU.GetValue().ToString().Trim(),
                this.CBH01_CHHWAMUL.GetValue().ToString().Trim(),
                this.TXT01_CHCHTANK.GetValue().ToString().Trim()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count <= 0)
            {
                this.ShowMessage("TY_M_UT_6AKCN443");
                this.TXT01_CHJGHWAJU.Focus();

                e.Successed = false;
                return;
            }


            // 입고 탱크번호 체크
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_66SDH426", this.TXT01_CHIPTANK.GetValue().ToString().Trim());

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count <= 0)
            {
                this.ShowMessage("TY_M_UT_6CMBL162");
                SetFocus(this.TXT01_CHIPTANK);

                e.Successed = false;
                return;
            }

            // 매출 입고파일 체크
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_6CMBO163",
                Get_Date(this.DTP01_CHIPHANG.GetValue().ToString()),
                this.CBH01_CHBONSUN.GetValue().ToString().Trim(),
                this.CBH01_CHHWAJU.GetValue().ToString().Trim(),
                this.CBH01_CHHWAMUL.GetValue().ToString().Trim(),
                this.TXT01_CHIPTANK.GetValue().ToString().Trim()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count <= 0)
            {
                this.ShowMessage("TY_M_UT_6AKCO445");
                SetFocus(this.TXT01_CHJGHWAJU);

                e.Successed = false;
                return;
            }
            else
            {
                this.TXT01_CHCONTNO.SetValue(dt.Rows[0]["COCONTNO"].ToString());
            }

            // 계약번호 체크
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_6CMDI168", this.TXT01_CHCONTNO.GetValue().ToString());

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count <= 0)
            {
                this.ShowMessage("TY_M_UT_6CMGS176");
                SetFocus(this.TXT01_CHJGHWAJU);

                e.Successed = false;
                return;
            }


            if (int.Parse(Get_Numeric(Get_Date(this.DTP01_CHENDIL.GetValue().ToString().Trim()))) == 0)
            {
                this.DTP01_CHENDIL.SetValue(this.DTP01_CHCHULIL.GetValue().ToString().Trim());
            }

            if (int.Parse(Get_Numeric(Get_Date(this.DTP01_CHCHULIL.GetValue().ToString().Trim()))) > int.Parse(Get_Numeric(Get_Date(this.DTP01_CHENDIL.GetValue().ToString().Trim()))))
            {
                this.ShowMessage("TY_M_UT_6CMGV177");
                SetFocus(this.DTP01_CHENDIL);

                e.Successed = false;
                return;
            }

            string sCHSTR = string.Empty;
            string sCHEND = string.Empty;
            sCHSTR = Set_Fill2(this.TXT01_CHCHSTR1.GetValue().ToString().Trim()) + Set_Fill2(this.TXT01_CHCHSTR2.GetValue().ToString().Trim());
            sCHEND = Set_Fill2(this.TXT01_CHCHEND1.GetValue().ToString().Trim()) + Set_Fill2(this.TXT01_CHCHEND2.GetValue().ToString().Trim());

            if (int.Parse(sCHSTR) > 2459 || int.Parse(Get_Numeric(sCHSTR)) < 0)
            {
                this.ShowMessage("TY_M_UT_6CMGV178");
                SetFocus(this.TXT01_CHCHSTR1);

                e.Successed = false;
                return;
            }

            if (int.Parse(sCHEND) > 2459 || int.Parse(Get_Numeric(sCHEND)) < 0)
            {
                this.ShowMessage("TY_M_UT_6CMGW179");
                SetFocus(this.TXT01_CHCHEND1);

                e.Successed = false;
                return;
            }


            if (double.Parse(Get_Numeric(this.TXT01_CHQTY.GetValue().ToString().Trim())) < 0)
            {
                this.ShowMessage("TY_M_UT_6CMGX180");
                SetFocus(this.TXT01_CHQTY);

                e.Successed = false;
                return;
            }

            if (double.Parse(Get_Numeric(this.TXT01_CHMTQTY.GetValue().ToString().Trim())) < 0)
            {
                this.ShowMessage("TY_M_UT_6CMGX180");
                SetFocus(this.TXT01_CHMTQTY);

                e.Successed = false;
                return;
            }

            if (double.Parse(Get_Numeric(this.TXT01_CHQTY.GetValue().ToString().Trim())) == 0)
            {
                if (double.Parse(Get_Numeric(this.TXT01_CHMTQTY.GetValue().ToString().Trim())) == 0)
                {
                    this.ShowMessage("TY_M_UT_6CMGX180");
                    SetFocus(this.TXT01_CHQTY);

                    e.Successed = false;
                    return;
                }
            }

            if (double.Parse(Get_Numeric(this.TXT01_CHOVQTY.GetValue().ToString().Trim())) < 0)
            {
                this.ShowMessage("TY_M_UT_6CMGY181");
                SetFocus(this.TXT01_CHOVQTY);

                e.Successed = false;
                return;
            }

            if (double.Parse(Get_Numeric(this.TXT01_CHOVQTY.GetValue().ToString().Trim())) > double.Parse(Get_Numeric(this.TXT01_CHMTQTY.GetValue().ToString().Trim())))
            {
                this.ShowMessage("TY_M_UT_6CMGY181");
                SetFocus(this.TXT01_CHOVQTY);

                e.Successed = false;
                return;
            }

            string sCOVSTR = string.Empty;
            string sCOVEND = string.Empty;
            sCOVSTR = Set_Fill2(this.TXT01_CHOVSTR1.GetValue().ToString().Trim()) + Set_Fill2(this.TXT01_CHOVSTR2.GetValue().ToString().Trim());
            sCOVEND = Set_Fill2(this.TXT01_CHOVEND1.GetValue().ToString().Trim()) + Set_Fill2(this.TXT01_CHOVEND2.GetValue().ToString().Trim());

            if (int.Parse(Get_Numeric(sCOVSTR)) > 2459 || int.Parse(Get_Numeric(sCOVSTR)) < 0)
            {
                this.ShowMessage("TY_M_UT_6CMGY182");
                SetFocus(this.TXT01_CHOVSTR1);

                e.Successed = false;
                return;
            }

            if (int.Parse(Get_Numeric(sCOVEND)) > 2459 || int.Parse(Get_Numeric(sCOVEND)) < 0)
            {
                this.ShowMessage("TY_M_UT_6CMGZ183");
                SetFocus(this.TXT01_CHOVEND1);

                e.Successed = false;
                return;
            }

            if (this.CBH01_CHCHULGB.GetValue().ToString().Trim() != "05")
            {
                if (this.CBH01_CHSUCHVS.GetValue().ToString().Trim() == "" && this.TXT01_CHCARNO.GetValue().ToString().Trim() == "")
                {
                    this.ShowMessage("TY_M_UT_6CMGZ184");
                    SetFocus(this.CBH01_CHSUCHVS.CodeText);

                    e.Successed = false;
                    return;
                }
            }

            if (Get_Numeric(Get_Date(this.DTP01_CHSUCHIP.GetValue().ToString().Trim())) == "0")
            {
                if (this.CBH01_CHSUCHVS.GetValue().ToString().Trim() != "")
                {
                    this.CBH01_CHSUCHVS.SetValue("");
                }
            }
            else
            {
                if (this.CBH01_CHSUCHVS.GetValue().ToString().Trim() == "")
                {
                    this.ShowMessage("TY_M_UT_6CMH1185");
                    SetFocus(this.CBH01_CHSUCHVS.CodeText);

                    e.Successed = false;
                    return;
                }
            }

            // 입고화물관리 선적, 재선적인 본선 조회 
            // 2021-06-04 김종술 차장 요청으로 주석처리
            //if (fsGUBUN.ToString() == "NEW")
            //{
            //    if (Get_Numeric(Get_Date(this.DTP01_CHSUCHIP.GetValue().ToString().Trim())) != "0" && this.CBH01_CHSUCHVS.GetValue().ToString().Trim() != "")
            //    {
            //        this.DbConnector.CommandClear();
            //        this.DbConnector.Attach
            //            (
            //            "TY_P_UT_6CMGS174",
            //            Get_Date(this.DTP01_CHIPHANG.GetValue().ToString()),
            //            this.CBH01_CHHWAJU.GetValue().ToString().Trim(),
            //            this.CBH01_CHHWAMUL.GetValue().ToString().Trim()
            //            );

            //        dt = this.DbConnector.ExecuteDataTable();

            //        if (dt.Rows.Count > 0)
            //        {
            //            this.CBH01_CHSUCHVS.SetValue(dt.Rows[0]["CMBONSUN"].ToString());
            //        }
            //    }
            //}


            if (this.CBO01_CHDANYI.GetValue().ToString().Trim() == "1")
            {
                // 20180111 고지파트에서 지시번호가 있을 경우
                // 지시의 내용과 다를 경우도 출고 등록되도록 해달라고 요청
                // 예) 입항, 본선, 화주, 화물 등...

                //if (Get_Date(this.TXT01_CHJISINUM1.GetValue().ToString().Trim()) != "")
                //{
                //    // 지시 내용과 일치하는지 체크
                //    this.DbConnector.CommandClear();
                //    this.DbConnector.Attach
                //        (
                //        "TY_P_UT_6CMH2187",
                //        Get_Date(this.TXT01_CHJISINUM1.GetValue().ToString().Trim()),
                //        this.TXT01_CHJISINUM2.GetValue().ToString().Trim(),
                //        Get_Date(this.DTP01_CHIPHANG.GetValue().ToString().Trim()),
                //        this.CBH01_CHBONSUN.GetValue().ToString().Trim().ToUpper(),
                //        this.CBH01_CHHWAJU.GetValue().ToString().Trim().ToUpper(),
                //        this.CBH01_CHHWAMUL.GetValue().ToString().Trim().ToUpper(),
                //        this.TXT01_CHBLNO.GetValue().ToString().Trim(),
                //        Get_Numeric(this.TXT01_CHMSNSEQ.GetValue().ToString().Trim()),
                //        Get_Numeric(this.TXT01_CHHSNSEQ.GetValue().ToString().Trim()),
                //        this.CBH01_CHACTHJ.GetValue().ToString().Trim().ToUpper(),
                //        Get_Date(this.DTP01_CHCUSTIL.GetValue().ToString().Trim()),
                //        Get_Numeric(this.TXT01_CHCHASU.GetValue().ToString().Trim()),
                //        this.TXT01_CHJGHWAJU.GetValue().ToString(),
                //        this.CBH01_CHYSHWAJU.GetValue().ToString(),
                //        this.CBH01_CHYDHWAJU.GetValue().ToString(),
                //        Get_Date(this.DTP01_CHYSDATE.GetValue().ToString().Trim()),
                //        this.TXT01_CHYDSEQ.GetValue().ToString(),
                //        this.TXT01_CHYSSEQ.GetValue().ToString()
                //        );

                //    dt = this.DbConnector.ExecuteDataTable();

                //    if (dt.Rows.Count <= 0)
                //    {
                //        this.ShowMessage("TY_M_UT_6CMHA188");
                //        SetFocus(this.TXT01_CHJGHWAJU);

                //        e.Successed = false;
                //        return;
                //    }                    
                //}
            }

            //// 드럼 출고
            //if (this.CBO01_CHDANYI.GetValue().ToString().Trim() == "2")
            //{
            //    if (Get_Date(this.TXT01_CHJISINUM1.GetValue().ToString().Trim()) != "")
            //    {
            //        // 지시 내용과 일치하는지 체크
            //        this.DbConnector.CommandClear();
            //        this.DbConnector.Attach
            //            (
            //            "TY_P_UT_6CMHC190",
            //            Get_Date(this.TXT01_CHJISINUM1.GetValue().ToString().Trim()),
            //            this.TXT01_CHJISINUM2.GetValue().ToString().Trim(),
            //            Get_Date(this.DTP01_CHIPHANG.GetValue().ToString().Trim()),
            //            this.CBH01_CHBONSUN.GetValue().ToString().Trim().ToUpper(),
            //            this.CBH01_CHHWAJU.GetValue().ToString().Trim().ToUpper(),
            //            this.CBH01_CHHWAMUL.GetValue().ToString().Trim().ToUpper(),
            //            this.TXT01_CHBLNO.GetValue().ToString().Trim(),
            //            Get_Numeric(this.TXT01_CHMSNSEQ.GetValue().ToString().Trim()),
            //            Get_Numeric(this.TXT01_CHHSNSEQ.GetValue().ToString().Trim()),
            //            this.CBH01_CHACTHJ.GetValue().ToString().Trim().ToUpper(),
            //            Get_Date(this.DTP01_CHCUSTIL.GetValue().ToString().Trim()),
            //            Get_Numeric(this.TXT01_CHCHASU.GetValue().ToString().Trim()),
            //            this.TXT01_CHJGHWAJU.GetValue().ToString(),
            //            this.CBH01_CHYSHWAJU.GetValue().ToString(),
            //            this.CBH01_CHYDHWAJU.GetValue().ToString(),
            //            Get_Date(this.DTP01_CHYSDATE.GetValue().ToString().Trim()),
            //            this.TXT01_CHYDSEQ.GetValue().ToString(),
            //            this.TXT01_CHYSSEQ.GetValue().ToString(),
            //            this.CBH01_CHCHHJ.GetValue().ToString().Trim()
            //            );

            //        dt = this.DbConnector.ExecuteDataTable();

            //        if (dt.Rows.Count <= 0)
            //        {
            //            this.ShowMessage("TY_M_UT_6CMHA188");
            //            SetFocus(this.TXT01_CHJGHWAJU);

            //            e.Successed = false;
            //            return;
            //        }
            //        else
            //        {
            //            double dJDJIQTY = 0;

            //            dJDJIQTY = double.Parse(String.Format("{0,9:N0}", dt.Rows[0]["JDJIQTY"].ToString()));

            //            if (double.Parse(String.Format("{0,9:N0}", dt.Rows[0]["JDJIQTY"].ToString())) < double.Parse(String.Format("{0,9:N0}", Get_Numeric(this.TXT01_CHQTY.GetValue().ToString()))))
            //            {
            //                this.ShowMessage("TY_M_UT_7CKDU344");
            //                SetFocus(this.TXT01_CHJISINUM1);

            //                e.Successed = false;
            //                return;
            //            }

            //            double dCHQTY = 0;
            //            string sCHJISINUM = string.Empty;

            //            sCHJISINUM = this.TXT01_CHJISINUM1.GetValue().ToString().Trim() + Set_Fill3(this.TXT01_CHJISINUM2.GetValue().ToString().Trim());

            //            // 지시 내용과 일치하는지 체크
            //            this.DbConnector.CommandClear();
            //            this.DbConnector.Attach
            //                (
            //                "TY_P_UT_7CKDW345",
            //                sCHJISINUM.ToString(),
            //                Get_Date(this.DTP01_CHIPHANG.GetValue().ToString().Trim()),
            //                this.CBH01_CHBONSUN.GetValue().ToString().Trim().ToUpper(),
            //                this.CBH01_CHHWAJU.GetValue().ToString().Trim().ToUpper(),
            //                this.CBH01_CHHWAMUL.GetValue().ToString().Trim().ToUpper(),
            //                this.TXT01_CHBLNO.GetValue().ToString().Trim(),
            //                Get_Numeric(this.TXT01_CHMSNSEQ.GetValue().ToString().Trim()),
            //                Get_Numeric(this.TXT01_CHHSNSEQ.GetValue().ToString().Trim()),
            //                Get_Date(this.DTP01_CHCUSTIL.GetValue().ToString().Trim()),
            //                Get_Numeric(this.TXT01_CHCHASU.GetValue().ToString().Trim()),
            //                this.CBH01_CHACTHJ.GetValue().ToString().Trim().ToUpper(),
            //                this.TXT01_CHJGHWAJU.GetValue().ToString(),
            //                this.CBH01_CHYSHWAJU.GetValue().ToString(),
            //                this.CBH01_CHYDHWAJU.GetValue().ToString(),
            //                Get_Date(this.DTP01_CHYSDATE.GetValue().ToString().Trim()),
            //                this.TXT01_CHYDSEQ.GetValue().ToString(),
            //                this.TXT01_CHYSSEQ.GetValue().ToString(),
            //                this.CBH01_CHCHHJ.GetValue().ToString().Trim()
            //                );

            //            dt = this.DbConnector.ExecuteDataTable();

            //            if (dt.Rows.Count > 0)
            //            {
            //                dCHQTY = double.Parse(String.Format("{0,9:N0}", dt.Rows[0]["CHQTY"].ToString()));
            //            }

            //            if (dJDJIQTY < dCHQTY + double.Parse(String.Format("{0,9:N0}", Get_Numeric(this.TXT01_CHQTY.GetValue().ToString()))) - double.Parse(String.Format("{0,9:N0}", fsCHQTY.ToString())))
            //            {
            //                this.ShowMessage("TY_M_UT_7CKE6346");
            //                SetFocus(this.TXT01_CHQTY);

            //                e.Successed = false;
            //                return;
            //            }
            //        }
            //    }
            //    else
            //    {
            //        this.ShowMessage("TY_M_UT_7CKBG339");
            //        SetFocus(this.TXT01_CHJISINUM1);

            //        e.Successed = false;
            //        return;
            //    }
            //}

            // 실차무계 = 공차무계 + 출고 M/T량
            this.TXT01_CHTOTAL.SetValue(Convert.ToString(double.Parse(Get_Numeric(this.TXT01_CHEMPTY.GetValue().ToString().Trim())) + double.Parse(Get_Numeric(this.TXT01_CHMTQTY.GetValue().ToString().Trim()))));

            if (this.CBO01_CHDANYI.GetValue().ToString().Trim() != "2")
            {
                if (fsGUBUN == "NEW")
                {
                    if (Decimal.Parse(String.Format("{0,9:N3}", Get_Numeric(fsSVCHQTY.ToString())))
                        + Decimal.Parse(String.Format("{0,9:N3}", Get_Numeric(this.TXT01_CHMTQTY.GetValue().ToString().Trim())))
                        > Decimal.Parse(String.Format("{0,9:N3}", Get_Numeric(fsSVMTQTY.ToString()))))
                    {
                        this.ShowMessage("TY_M_UT_6CMHC189");
                        this.TXT01_CHMTQTY.Focus();

                        e.Successed = false;
                        return;
                    }
                    else
                    {
                        fsSCCHJEQTY = (
                                         Decimal.Parse(String.Format("{0,9:N3}", Get_Numeric(fsSVMTQTY.ToString())))
                                       - Decimal.Parse(String.Format("{0,9:N3}", Get_Numeric(fsSVCHQTY.ToString())))
                                       - Decimal.Parse(String.Format("{0,9:N3}", Get_Numeric(this.TXT01_CHMTQTY.GetValue().ToString().Trim())))
                                       ).ToString("0.000");
                    }
                }
                else if (fsGUBUN == "UPT")
                {
                    if (fsCHCHTANK.ToString().Trim() == this.TXT01_CHCHTANK.GetValue().ToString().Trim())
                    {
                        if (Decimal.Parse(String.Format("{0,9:N3}", Get_Numeric(fsSVCHQTY.ToString())))
                            - Decimal.Parse(String.Format("{0,9:N3}", Get_Numeric(fsCHMTQTY.ToString())))
                            + Decimal.Parse(String.Format("{0,9:N3}", Get_Numeric(this.TXT01_CHMTQTY.GetValue().ToString().Trim())))
                            > Decimal.Parse(String.Format("{0,9:N3}", Get_Numeric(fsSVMTQTY.ToString()))))
                        {
                            this.ShowMessage("TY_M_UT_6CMHC189");
                            this.TXT01_CHMTQTY.Focus();

                            e.Successed = false;
                            return;
                        }
                        else
                        {
                            fsSCCHJEQTY =
                                (
                                   Decimal.Parse(String.Format("{0,9:N3}", Get_Numeric(fsSVMTQTY.ToString())))
                                 - Decimal.Parse(String.Format("{0,9:N3}", Get_Numeric(fsSVCHQTY.ToString())))
                                 + Decimal.Parse(String.Format("{0,9:N3}", Get_Numeric(fsCHMTQTY.ToString())))
                                 - Decimal.Parse(String.Format("{0,9:N3}", Get_Numeric(this.TXT01_CHMTQTY.GetValue().ToString().Trim())))
                                ).ToString("0.000");
                        }
                    }
                    else
                    {
                        if (Decimal.Parse(String.Format("{0,9:N3}", Get_Numeric(fsSVCHQTY.ToString())))
                            + Decimal.Parse(String.Format("{0,9:N3}", Get_Numeric(this.TXT01_CHMTQTY.GetValue().ToString().Trim())))
                            > Decimal.Parse(String.Format("{0,9:N3}", Get_Numeric(fsSVMTQTY.ToString()))))
                        {
                            this.ShowMessage("TY_M_UT_6CMHC189");
                            this.TXT01_CHMTQTY.Focus();

                            e.Successed = false;
                            return;
                        }
                        else
                        {
                            fsSCCHJEQTY =
                                (
                                   Decimal.Parse(String.Format("{0,9:N3}", Get_Numeric(fsSVMTQTY.ToString())))
                                 - Decimal.Parse(String.Format("{0,9:N3}", Get_Numeric(fsSVCHQTY.ToString())))
                                 - Decimal.Parse(String.Format("{0,9:N3}", Get_Numeric(this.TXT01_CHMTQTY.GetValue().ToString().Trim())))
                                 ).ToString("0.000");
                        }
                    }
                }
                else
                {
                    if (Decimal.Parse(String.Format("{0,9:N3}", Get_Numeric(Get_Numeric(fsSVCHQTY.ToString()))))
                        - Decimal.Parse(String.Format("{0,9:N3}", Get_Numeric(this.TXT01_CHMTQTY.GetValue().ToString().Trim()))) < 0)
                    {
                        this.ShowMessage("TY_M_UT_6CMHC189");
                        this.TXT01_CHMTQTY.Focus();

                        e.Successed = false;
                        return;
                    }
                    else
                    {
                        fsSCCHJEQTY =
                            (
                               Decimal.Parse(String.Format("{0,9:N3}", Get_Numeric(fsSVMTQTY.ToString())))
                             - Decimal.Parse(String.Format("{0,9:N3}", Get_Numeric(fsSVCHQTY.ToString())))
                             - Decimal.Parse(String.Format("{0,9:N3}", Get_Numeric(this.TXT01_CHMTQTY.GetValue().ToString().Trim())))
                             ).ToString("0.000");
                    }
                }
                // 마지막 출고 데이터 체크 - SURVEY KL량 과 출고 총 KL량 맞추는 작업 
                // 2021-07-05 박동근 차장 요청

                if (Decimal.Parse(String.Format("{0,9:N3}", Get_Numeric(fsSCCHJEQTY.ToString()))) == 0)
                {
                    this.DbConnector.CommandClear();

                    if (fsGUBUN == "NEW")
                    {
                        this.DbConnector.Attach
                            (
                            "TY_P_UT_B76DE435",
                            Get_Date(this.DTP01_CHIPHANG.GetValue().ToString()),
                            this.CBH01_CHBONSUN.GetValue().ToString().Trim(),
                            this.CBH01_CHHWAJU.GetValue().ToString().Trim(),
                            this.CBH01_CHHWAMUL.GetValue().ToString().Trim(),
                            this.TXT01_CHIPTANK.GetValue().ToString().Trim()
                            );
                    }
                    else
                    {
                        this.DbConnector.Attach
                            (
                            "TY_P_UT_B83AC490",
                            Get_Date(this.DTP01_CHCHULIL.GetValue().ToString()) + this.TXT01_CHTKNO.GetValue().ToString(),
                            Get_Date(this.DTP01_CHIPHANG.GetValue().ToString()),
                            this.CBH01_CHBONSUN.GetValue().ToString().Trim(),
                            this.CBH01_CHHWAJU.GetValue().ToString().Trim(),
                            this.CBH01_CHHWAMUL.GetValue().ToString().Trim(),
                            this.TXT01_CHIPTANK.GetValue().ToString().Trim()
                            );
                    }

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        if (Convert.ToDouble(Get_Numeric(dt.Rows[0]["KLQTY"].ToString())) > 0)
                        {
                            this.TXT01_CHKLQTY.SetValue(Get_Numeric(dt.Rows[0]["KLQTY"].ToString()));
                        }
                    }
                }
            }

            // 매출입고 할증파일
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_6CMBO163",
                Get_Date(this.DTP01_CHIPHANG.GetValue().ToString()),
                this.CBH01_CHBONSUN.GetValue().ToString().Trim(),
                this.CBH01_CHHWAJU.GetValue().ToString().Trim(),
                this.CBH01_CHHWAMUL.GetValue().ToString().Trim(),
                this.TXT01_CHIPTANK.GetValue().ToString().Trim()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count <= 0)
            {
                fsMTQTY = "0";
                fsCHQTY = "0";
            }
            else
            {
                fsMTQTY = Get_Numeric(dt.Rows[0]["COMTQTY"].ToString());
                fsCHQTY = Get_Numeric(dt.Rows[0]["COCHQTY"].ToString());
            }

            if (fsGUBUN == "NEW")
            {
                if (Decimal.Parse(String.Format("{0,9:N3}", Get_Numeric(fsCHQTY.ToString())))
                    + Decimal.Parse(String.Format("{0,9:N3}", Get_Numeric(this.TXT01_CHMTQTY.GetValue().ToString().Trim())))
                    > Decimal.Parse(String.Format("{0,9:N3}", Get_Numeric(fsMTQTY.ToString()))))
                {
                    this.ShowMessage("TY_M_UT_6CMHC189");
                    this.TXT01_CHMTQTY.Focus();

                    e.Successed = false;
                    return;
                }
                else
                {
                    fsSCIPJEQTY =
                        (
                           Decimal.Parse(String.Format("{0,9:N3}", Get_Numeric(fsMTQTY.ToString())))
                         - Decimal.Parse(String.Format("{0,9:N3}", Get_Numeric(fsCHQTY.ToString())))
                         - Decimal.Parse(String.Format("{0,9:N3}", Get_Numeric(this.TXT01_CHMTQTY.GetValue().ToString().Trim())))
                         ).ToString("0.000");
                }
            }
            else if (fsGUBUN == "UPT")
            {
                if (fsCHIPTANK.ToString().Trim() == this.TXT01_CHIPTANK.GetValue().ToString().Trim())
                {
                    if (Decimal.Parse(String.Format("{0,9:N3}", Get_Numeric(fsCHQTY.ToString())))
                        - Decimal.Parse(String.Format("{0,9:N3}", Get_Numeric(fsCHMTQTY.ToString())))
                        + Decimal.Parse(String.Format("{0,9:N3}", Get_Numeric(this.TXT01_CHMTQTY.GetValue().ToString().Trim())))
                        > Decimal.Parse(String.Format("{0,9:N3}", Get_Numeric(fsMTQTY.ToString()))))
                    {
                        this.ShowMessage("TY_M_UT_6CMHC189");
                        this.TXT01_CHMTQTY.Focus();

                        e.Successed = false;
                        return;
                    }
                    else
                    {
                        fsSCIPJEQTY =
                            (
                               Decimal.Parse(String.Format("{0,9:N3}", Get_Numeric(fsMTQTY.ToString())))
                             - Decimal.Parse(String.Format("{0,9:N3}", Get_Numeric(fsCHQTY.ToString())))
                             + Decimal.Parse(String.Format("{0,9:N3}", Get_Numeric(fsCHMTQTY.ToString())))
                             - Decimal.Parse(String.Format("{0,9:N3}", Get_Numeric(this.TXT01_CHMTQTY.GetValue().ToString().Trim())))
                            ).ToString("0.000");
                    }
                }
                else
                {
                    if (Decimal.Parse(String.Format("{0,9:N3}", Get_Numeric(fsCHQTY.ToString())))
                        + Decimal.Parse(String.Format("{0,9:N3}", Get_Numeric(this.TXT01_CHMTQTY.GetValue().ToString().Trim())))
                        > Decimal.Parse(String.Format("{0,9:N3}", Get_Numeric(fsMTQTY.ToString()))))
                    {
                        this.ShowMessage("TY_M_UT_6CMHC189");
                        this.TXT01_CHMTQTY.Focus();

                        e.Successed = false;
                        return;
                    }
                    else
                    {
                        fsSCIPJEQTY =
                            (
                              double.Parse(String.Format("{0,9:N3}", Get_Numeric(fsMTQTY.ToString())))
                            - double.Parse(String.Format("{0,9:N3}", Get_Numeric(fsCHQTY.ToString())))
                            - double.Parse(String.Format("{0,9:N3}", Get_Numeric(this.TXT01_CHMTQTY.GetValue().ToString().Trim())))
                            ).ToString("0.000");
                    }
                }
            }
            else
            {
                if (Decimal.Parse(String.Format("{0,9:N3}", Get_Numeric(fsCHQTY.ToString())))
                    - Decimal.Parse(String.Format("{0,9:N3}", Get_Numeric(this.TXT01_CHMTQTY.GetValue().ToString().Trim())))
                    < 0)
                {
                    this.ShowMessage("TY_M_UT_6CMHC189");
                    this.TXT01_CHMTQTY.Focus();

                    e.Successed = false;
                    return;
                }
                else
                {
                    fsSCCHJEQTY =
                        (
                           Decimal.Parse(String.Format("{0,9:N3}", Get_Numeric(fsMTQTY.ToString())))
                         - Decimal.Parse(String.Format("{0,9:N3}", Get_Numeric(fsCHQTY.ToString())))
                         - Decimal.Parse(String.Format("{0,9:N3}", Get_Numeric(this.TXT01_CHMTQTY.GetValue().ToString().Trim())))
                         ).ToString("0.000");
                }
            }

            // B/L별입고 관리 체크
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_6CMHW191",
                Get_Date(this.DTP01_CHIPHANG.GetValue().ToString()),
                this.CBH01_CHBONSUN.GetValue().ToString().Trim(),
                this.CBH01_CHHWAJU.GetValue().ToString().Trim(),
                this.CBH01_CHHWAMUL.GetValue().ToString().Trim(),
                this.TXT01_CHBLNO.GetValue().ToString().Trim(),
                Get_Numeric(this.TXT01_CHMSNSEQ.GetValue().ToString().Trim()),
                Get_Numeric(this.TXT01_CHHSNSEQ.GetValue().ToString().Trim())
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                fsMTQTY = dt.Rows[0]["IPMTQTY"].ToString();
                fsCHQTY = dt.Rows[0]["IPCHQTY"].ToString();
            }
            else
            {
                fsMTQTY = "0";
                fsCHQTY = "0";
            }

            if (fsGUBUN == "NEW")
            {
                if (Decimal.Parse(String.Format("{0,9:N3}", Get_Numeric(fsCHQTY.ToString())))
                    + Decimal.Parse(String.Format("{0,9:N3}", Get_Numeric(this.TXT01_CHMTQTY.GetValue().ToString().Trim())))
                    > Decimal.Parse(String.Format("{0,9:N3}", Get_Numeric(fsMTQTY.ToString()))))
                {
                    this.ShowMessage("TY_M_UT_6CMHZ193");
                    this.TXT01_CHMTQTY.Focus();

                    e.Successed = false;
                    return;
                }
            }
            else if (fsGUBUN == "UPT")
            {
                if (Decimal.Parse(String.Format("{0,9:N3}", Get_Numeric(fsCHQTY.ToString())))
                    - Decimal.Parse(String.Format("{0,9:N3}", Get_Numeric(fsCHMTQTY.ToString())))
                    + Decimal.Parse(String.Format("{0,9:N3}", Get_Numeric(this.TXT01_CHMTQTY.GetValue().ToString().Trim())))
                    > Decimal.Parse(String.Format("{0,9:N3}", Get_Numeric(fsMTQTY.ToString()))))
                {
                    this.ShowMessage("TY_M_UT_6CMHZ193");
                    this.TXT01_CHMTQTY.Focus();

                    e.Successed = false;
                    return;
                }
            }
            else
            {
                if (Decimal.Parse(String.Format("{0,9:N3}", Get_Numeric(fsCHQTY.ToString())))
                    - Decimal.Parse(String.Format("{0,9:N3}", Get_Numeric(this.TXT01_CHMTQTY.GetValue().ToString().Trim())))
                    < 0)
                {
                    this.ShowMessage("TY_M_UT_6CMHZ193");
                    this.TXT01_CHMTQTY.Focus();

                    e.Successed = false;
                    return;
                }
            }

            string sCJYSQTY = string.Empty;
            string sCJYDQTY = string.Empty;
            string sCJYSYDQTY = string.Empty;
            string sCJYSCHQTY = string.Empty;

            // 통관화주별 재고 체크
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_6CMI1194",
                this.CBH01_CHACTHJ.GetValue().ToString().Trim().ToUpper(),
                Get_Date(this.DTP01_CHIPHANG.GetValue().ToString()),
                this.CBH01_CHBONSUN.GetValue().ToString().Trim(),
                this.CBH01_CHHWAJU.GetValue().ToString().Trim(),
                this.CBH01_CHHWAMUL.GetValue().ToString().Trim(),
                this.TXT01_CHBLNO.GetValue().ToString().Trim(),
                Get_Numeric(this.TXT01_CHMSNSEQ.GetValue().ToString().Trim()),
                Get_Numeric(this.TXT01_CHHSNSEQ.GetValue().ToString().Trim()),
                this.TXT01_CHJGHWAJU.GetValue().ToString(),
                Get_Date(this.DTP01_CHCUSTIL.GetValue().ToString().Trim()),
                Get_Numeric(this.TXT01_CHCHASU.GetValue().ToString().Trim()),
                this.CBH01_CHYSHWAJU.GetValue().ToString(),
                this.CBH01_CHYDHWAJU.GetValue().ToString(),
                Get_Date(this.DTP01_CHYSDATE.GetValue().ToString().Trim()),
                this.TXT01_CHYDSEQ.GetValue().ToString(),
                this.TXT01_CHYSSEQ.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                fsMTQTY = dt.Rows[0]["CJCUQTY"].ToString();   // 통관량
                sCJYSQTY = dt.Rows[0]["CJYSQTY"].ToString();   // 양수량
                sCJYDQTY = dt.Rows[0]["CJYDQTY"].ToString();   // 양도량
                sCJYSYDQTY = dt.Rows[0]["CJYSYDQTY"].ToString(); // 양수양도량
                fsCHQTY = dt.Rows[0]["CJCHQTY"].ToString();   // 출고량                
                sCJYSCHQTY = dt.Rows[0]["CJYSCHQTY"].ToString(); // 양수출고량
            }
            else
            {
                fsMTQTY = "0";
                fsCHQTY = "0";
            }

            if (fsGUBUN == "NEW")
            {
                if ((Decimal.Parse(String.Format("{0,9:N3}", Get_Numeric(sCJYDQTY.ToString())))
                     + Decimal.Parse(String.Format("{0,9:N3}", Get_Numeric(sCJYSYDQTY.ToString())))
                     + Decimal.Parse(String.Format("{0,9:N3}", Get_Numeric(sCJYSCHQTY.ToString())))
                     + Decimal.Parse(String.Format("{0,9:N3}", Get_Numeric(fsCHQTY.ToString())))
                     + Decimal.Parse(String.Format("{0,9:N3}", Get_Numeric(this.TXT01_CHMTQTY.GetValue().ToString().Trim())))
                    )
                    >
                    (Decimal.Parse(String.Format("{0,9:N3}", Get_Numeric(fsMTQTY.ToString())))
                     + Decimal.Parse(String.Format("{0,9:N3}", Get_Numeric(sCJYSQTY.ToString())))
                    )
                   )
                {
                    this.ShowMessage("TY_M_UT_6CMHY192");
                    this.TXT01_CHMTQTY.Focus();

                    e.Successed = false;
                    return;
                }
            }
            else if (fsGUBUN == "UPT")
            {
                if ((Decimal.Parse(String.Format("{0,9:N3}", Get_Numeric(sCJYDQTY.ToString())))
                     + Decimal.Parse(String.Format("{0,9:N3}", Get_Numeric(sCJYSYDQTY.ToString())))
                     + Decimal.Parse(String.Format("{0,9:N3}", Get_Numeric(sCJYSCHQTY.ToString())))
                     + Decimal.Parse(String.Format("{0,9:N3}", Get_Numeric(fsCHQTY.ToString())))
                     - Decimal.Parse(String.Format("{0,9:N3}", Get_Numeric(fsCHMTQTY.ToString())))
                     + Decimal.Parse(String.Format("{0,9:N3}", Get_Numeric(this.TXT01_CHMTQTY.GetValue().ToString().Trim()))))
                    >
                    (Decimal.Parse(String.Format("{0,9:N3}", Get_Numeric(fsMTQTY.ToString())))
                     + Decimal.Parse(String.Format("{0,9:N3}", Get_Numeric(sCJYSQTY.ToString())))
                    )
                   )
                {
                    this.ShowMessage("TY_M_UT_6CMHY192");
                    this.TXT01_CHMTQTY.Focus();

                    e.Successed = false;
                    return;
                }
            }
            else
            {
                if ((Decimal.Parse(String.Format("{0,9:N3}", Get_Numeric(sCJYDQTY.ToString())))
                     + Decimal.Parse(String.Format("{0,9:N3}", Get_Numeric(sCJYSYDQTY.ToString())))
                     + Decimal.Parse(String.Format("{0,9:N3}", Get_Numeric(sCJYSCHQTY.ToString())))
                     + Decimal.Parse(String.Format("{0,9:N3}", Get_Numeric(fsCHQTY.ToString())))
                     - Decimal.Parse(String.Format("{0,9:N3}", Get_Numeric(fsCHMTQTY.ToString())))
                     + Decimal.Parse(String.Format("{0,9:N3}", Get_Numeric(this.TXT01_CHMTQTY.GetValue().ToString().Trim()))))
                    < 0)
                {
                    this.ShowMessage("TY_M_UT_6CMHY192");
                    this.TXT01_CHMTQTY.Focus();

                    e.Successed = false;
                    return;
                }
            }

            // 통관파일 체크
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_6CMI6195",
                Get_Date(this.DTP01_CHIPHANG.GetValue().ToString()),
                this.CBH01_CHBONSUN.GetValue().ToString().Trim(),
                this.CBH01_CHHWAJU.GetValue().ToString().Trim(),
                this.CBH01_CHHWAMUL.GetValue().ToString().Trim(),
                this.TXT01_CHBLNO.GetValue().ToString().Trim(),
                Get_Numeric(this.TXT01_CHMSNSEQ.GetValue().ToString().Trim()),
                Get_Numeric(this.TXT01_CHHSNSEQ.GetValue().ToString().Trim()),
                Get_Date(this.DTP01_CHCUSTIL.GetValue().ToString().Trim()),
                Get_Numeric(this.TXT01_CHCHASU.GetValue().ToString().Trim())
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                fsMTQTY = dt.Rows[0]["CSCUQTY"].ToString();
                fsCHQTY = dt.Rows[0]["CSCHQTY"].ToString();
            }
            else
            {
                fsMTQTY = "0";
                fsCHQTY = "0";
            }

            if (fsGUBUN == "NEW")
            {
                if (Decimal.Parse(String.Format("{0,9:N3}", Get_Numeric(fsCHQTY.ToString())))
                    + Decimal.Parse(String.Format("{0,9:N3}", Get_Numeric(this.TXT01_CHMTQTY.GetValue().ToString().Trim())))
                    > Decimal.Parse(String.Format("{0,9:N3}", Get_Numeric(fsMTQTY.ToString()))))
                {
                    this.ShowMessage("TY_M_UT_6CMHY192");
                    this.TXT01_CHMTQTY.Focus();

                    e.Successed = false;
                    return;
                }
            }
            else if (fsGUBUN == "UPT")
            {
                if (Decimal.Parse(String.Format("{0,9:N3}", Get_Numeric(fsCHQTY.ToString())))
                    - Decimal.Parse(String.Format("{0,9:N3}", Get_Numeric(fsCHMTQTY.ToString())))
                    + Decimal.Parse(String.Format("{0,9:N3}", Get_Numeric(this.TXT01_CHMTQTY.GetValue().ToString().Trim())))
                    > Decimal.Parse(String.Format("{0,9:N3}", Get_Numeric(fsMTQTY.ToString()))))
                {
                    this.ShowMessage("TY_M_UT_6CMHY192");
                    this.TXT01_CHMTQTY.Focus();

                    e.Successed = false;
                    return;
                }
            }
            else
            {
                if (Decimal.Parse(String.Format("{0,9:N3}", Get_Numeric(fsCHQTY.ToString())))
                    - Decimal.Parse(String.Format("{0,9:N3}", Get_Numeric(this.TXT01_CHMTQTY.GetValue().ToString().Trim())))
                    < 0)
                {
                    this.ShowMessage("TY_M_UT_6CMHY192");
                    this.TXT01_CHMTQTY.Focus();

                    e.Successed = false;
                    return;
                }
            }

            fsCHYNGUBUN = "";

            if (this.CBH01_CHYSHWAJU.GetValue().ToString() != "" && this.CBH01_CHYDHWAJU.GetValue().ToString() != "" &&
                Get_Date(this.DTP01_CHYSDATE.GetValue().ToString().Trim()) != "0" && Get_Numeric(this.TXT01_CHYDSEQ.GetValue().ToString()) != "0" &&
                Get_Numeric(this.TXT01_CHYSSEQ.GetValue().ToString()) != "0")
            {
                fsCHYNGUBUN = "R";

                fsCHYNCHQTY_AFT = this.TXT01_CHMTQTY.GetValue().ToString();

                string sYNQTY = string.Empty;
                string sYNYSCHQTY = string.Empty;

                // 양수도 파일 체크
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_UT_6CNG4200",
                    Get_Date(this.DTP01_CHIPHANG.GetValue().ToString()),
                    this.CBH01_CHBONSUN.GetValue().ToString().Trim(),
                    this.CBH01_CHHWAJU.GetValue().ToString().Trim(),
                    this.CBH01_CHHWAMUL.GetValue().ToString().Trim(),
                    this.TXT01_CHBLNO.GetValue().ToString().Trim(),
                    Get_Numeric(this.TXT01_CHMSNSEQ.GetValue().ToString().Trim()),
                    Get_Numeric(this.TXT01_CHHSNSEQ.GetValue().ToString().Trim()),
                    Get_Date(this.DTP01_CHCUSTIL.GetValue().ToString().Trim()),
                    Get_Numeric(this.TXT01_CHCHASU.GetValue().ToString().Trim()),
                    this.CBH01_CHACTHJ.GetValue().ToString().Trim().ToUpper(),
                    this.CBH01_CHYSHWAJU.GetValue().ToString(),
                    this.CBH01_CHYDHWAJU.GetValue().ToString(),
                    Get_Date(this.DTP01_CHYSDATE.GetValue().ToString().Trim()),
                    this.TXT01_CHYDSEQ.GetValue().ToString(),
                    this.TXT01_CHYSSEQ.GetValue().ToString()
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count <= 0)
                {
                    fsMTQTY = "0";
                    fsCHQTY = "";
                }
                else
                {
                    fsMTQTY = dt.Rows[0]["YNQTY"].ToString();
                    fsCHQTY = dt.Rows[0]["YNYSCHQTY"].ToString();
                }

                if (fsGUBUN == "NEW")
                {
                    if (Decimal.Parse(String.Format("{0,9:N3}", Get_Numeric(fsCHQTY.ToString())))
                        + Decimal.Parse(String.Format("{0,9:N3}", Get_Numeric(this.TXT01_CHMTQTY.GetValue().ToString().Trim())))
                        > Decimal.Parse(String.Format("{0,9:N3}", Get_Numeric(fsMTQTY.ToString()))))
                    {
                        this.ShowMessage("TY_M_UT_6CNG0201");
                        this.TXT01_CHMTQTY.Focus();

                        e.Successed = false;
                        return;
                    }
                }
                else if (fsGUBUN == "UPT")
                {
                    if (Decimal.Parse(String.Format("{0,9:N3}", Get_Numeric(fsCHQTY.ToString())))
                        - Decimal.Parse(String.Format("{0,9:N3}", Get_Numeric(fsCHMTQTY.ToString())))
                        + Decimal.Parse(String.Format("{0,9:N3}", Get_Numeric(this.TXT01_CHMTQTY.GetValue().ToString().Trim())))
                        > Decimal.Parse(String.Format("{0,9:N3}", Get_Numeric(fsMTQTY.ToString()))))
                    {
                        this.ShowMessage("TY_M_UT_6CNG0201");
                        this.TXT01_CHMTQTY.Focus();

                        e.Successed = false;
                        return;
                    }
                }
                else
                {
                    if (Decimal.Parse(String.Format("{0,9:N3}", Get_Numeric(fsCHQTY.ToString())))
                        - Decimal.Parse(String.Format("{0,9:N3}", Get_Numeric(this.TXT01_CHMTQTY.GetValue().ToString().Trim())))
                        < 0)
                    {
                        this.ShowMessage("TY_M_UT_6CNG0201");
                        this.TXT01_CHMTQTY.Focus();

                        e.Successed = false;
                        return;
                    }
                }
            }








            if (this.CBH01_CHCHHJ.GetValue().ToString().Trim() == "")
            {
                this.CBH01_CHCHHJ.SetValue(this.CBH01_CHACTHJ.GetValue().ToString().Trim());
            }

            if (Get_Date(this.DTP01_CHSUCHIP.GetValue().ToString().Trim()) != "" && this.CBH01_CHSUCHVS.GetValue().ToString().Trim() != "")
            {
                string sHWAJU = string.Empty;
                if (this.CBH01_CHHWAJU.GetValue().ToString().Trim() == "KPC")
                {
                    sHWAJU = "KJH";
                }
                else
                {
                    sHWAJU = this.CBH01_CHHWAJU.GetValue().ToString().Trim();
                }
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
            fsCHIPHANG = "";
            fsCHBONSUN = "";
            fsCHHWAJU = "";
            fsCHHWAMUL = "";
            fsCHBLNO = "";
            fsCHMSNSEQ = "";
            fsCHHSNSEQ = "";
            fsCHACTHJ = "";
            fsCHCUSTIL = "";
            fsCHCHASU = "";
            fsCHCHTANK = "";
            fsCHJGHWAJU = "";
            fsCHYSHWAJU = "";
            fsCHYDHWAJU = "";
            fsCHYSDATE = "";
            fsCHYDSEQ = "";
            fsCHYSSEQ = "";
            fsCHIPTANK = "";

            fsSVMTQTY = "";
            fsSVCHQTY = "";
            fsSVKLQTY = "";
            fsSVBIJUNG = "";

            fsKLQTY = "";
            fsMTQTY = "";

            fsDJCHQTY = "";
            fsDJPOQTY = "";
            fsDJJEQTY = "";

            fsDTCHQTY = "";
            fsDTPOQTY = "";
            fsDTJEQTY = "";

            fsCONVERT = "";
            fsOVER_KL = "";
            fsKESAN = "";
            fsCHHWAPE = "";
            fsCHMTQTY = "";
            fsCHYNCHQTY_AGO = "";
            fsCHKLQTY = "";
            fsCHJISINUM = "";
            fsCHQTY = "";

            fsCHJUNG = "";
            fsCHCHHJ = "";
            fsCHBIJUNG = "";
            fsCHVCF = "";
            fsSCCHJEQTY = "";
            fsSCIPJEQTY = "";
            fsCHDANYI = "";

            fsCHCHULGB = "";

            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_6CLD4142",
                Get_Date(this.DTP01_CHCHULIL.GetValue().ToString()), // 출고일자
                this.TXT01_CHTKNO.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                fsCHIPHANG = dt.Rows[0]["CHIPHANG"].ToString();
                fsCHBONSUN = dt.Rows[0]["CHBONSUN"].ToString();
                fsCHHWAJU = dt.Rows[0]["CHHWAJU"].ToString();
                fsCHHWAMUL = dt.Rows[0]["CHHWAMUL"].ToString();
                fsCHBLNO = dt.Rows[0]["CHBLNO"].ToString();
                fsCHMSNSEQ = dt.Rows[0]["CHMSNSEQ"].ToString();
                fsCHHSNSEQ = dt.Rows[0]["CHHSNSEQ"].ToString();
                fsCHACTHJ = dt.Rows[0]["CHACTHJ"].ToString();
                fsCHCUSTIL = dt.Rows[0]["CHCUSTIL"].ToString();
                fsCHCHASU = dt.Rows[0]["CHCHASU"].ToString();

                fsCHJGHWAJU = dt.Rows[0]["CHJGHWAJU"].ToString();
                fsCHYSHWAJU = dt.Rows[0]["CHYSHWAJU"].ToString();
                fsCHYDHWAJU = dt.Rows[0]["CHYDHWAJU"].ToString();
                fsCHYSDATE = dt.Rows[0]["CHYSDATE"].ToString();
                fsCHYDSEQ = dt.Rows[0]["CHYDSEQ"].ToString();
                fsCHYSSEQ = dt.Rows[0]["CHYSSEQ"].ToString();

                fsCHCHTANK = dt.Rows[0]["CHCHTANK"].ToString();
                fsCHIPTANK = dt.Rows[0]["CHIPTANK"].ToString();
                fsCHCHULGB = dt.Rows[0]["CHCHULGB"].ToString();
                fsCHMTQTY = dt.Rows[0]["CHMTQTY"].ToString();
                fsCHYNCHQTY_AGO = dt.Rows[0]["CHYNCHQTY"].ToString();
                fsCHKLQTY = dt.Rows[0]["CHKLQTY"].ToString();
                fsCHDANYI = dt.Rows[0]["CHDANYI"].ToString();
                fsCHJUNG = dt.Rows[0]["CHJUNG"].ToString();
                fsCHDRQTY = dt.Rows[0]["CHQTY"].ToString();
                fsCHCHHJ = dt.Rows[0]["CHCHHJ"].ToString();
                fsCHHWAPE = dt.Rows[0]["CHHWAPE"].ToString();
                fsCHJISINUM = dt.Rows[0]["CHJISINUM"].ToString();
                fsCHHIGB = dt.Rows[0]["CHHIGB"].ToString();
                fsCHCHTANK = dt.Rows[0]["CHCHTANK"].ToString();
                fsCHIPTANK = dt.Rows[0]["CHIPTANK"].ToString();
            }

            // 출고 - SURVEY 체크
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_6CLG1146",
                fsCHIPHANG.ToString().Trim(),
                fsCHBONSUN.ToString().Trim(),
                fsCHHWAJU.ToString().Trim(),
                fsCHHWAMUL.ToString().Trim(),
                fsCHCHTANK.ToString().Trim()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                fsMTQTY = dt.Rows[0]["SVMTQTY"].ToString();
                fsCHQTY = dt.Rows[0]["SVCHULQTY"].ToString();
                fsSVKLQTY = dt.Rows[0]["SVKLQTY"].ToString();
                fsSVBIJUNG = dt.Rows[0]["SVBIJUNG"].ToString();
            }
            else
            {
                fsMTQTY = "0";
                fsCHQTY = "0";
            }

            fsSCCHJEQTY = (
                             double.Parse(String.Format("{0,9:N3}", fsMTQTY.ToString()))
                           - double.Parse(String.Format("{0,9:N3}", fsCHQTY.ToString()))
                          ).ToString("0.000");

            // 매출입고 할증파일
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_6CMBO163",
                fsCHIPHANG.ToString().Trim(),
                fsCHBONSUN.ToString().Trim(),
                fsCHHWAJU.ToString().Trim(),
                fsCHHWAMUL.ToString().Trim(),
                fsCHIPTANK.ToString().Trim()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count <= 0)
            {
                fsMTQTY = "0";
                fsCHQTY = "0";
            }
            else
            {
                fsMTQTY = Get_Numeric(dt.Rows[0]["COMTQTY"].ToString());
                fsCHQTY = Get_Numeric(dt.Rows[0]["COCHQTY"].ToString());
            }



            fsSCIPJEQTY = (
                             double.Parse(String.Format("{0,9:N3}", fsMTQTY.ToString()))
                           - double.Parse(String.Format("{0,9:N3}", fsCHQTY.ToString()))
                          ).ToString("0.000");


            if (Get_Date(this.DTP01_CHIPHANG.GetValue().ToString().Trim()) != fsCHIPHANG.ToString().Trim() ||
                    this.CBH01_CHBONSUN.GetValue().ToString().Trim().ToUpper() != fsCHBONSUN.ToString().Trim() ||
                    this.CBH01_CHHWAJU.GetValue().ToString().Trim().ToUpper() != fsCHHWAJU.ToString().Trim() ||
                    this.CBH01_CHHWAMUL.GetValue().ToString().Trim().ToUpper() != fsCHHWAMUL.ToString().Trim() ||
                    this.TXT01_CHBLNO.GetValue().ToString().Trim() != fsCHBLNO.ToString().Trim() ||
                    Get_Numeric(this.TXT01_CHMSNSEQ.GetValue().ToString().Trim()) != fsCHMSNSEQ.ToString().Trim() ||
                    Get_Numeric(this.TXT01_CHHSNSEQ.GetValue().ToString().Trim()) != fsCHHSNSEQ.ToString().Trim() ||
                    this.CBH01_CHACTHJ.GetValue().ToString() != fsCHACTHJ.ToString().Trim() ||
                    Get_Date(this.DTP01_CHCUSTIL.GetValue().ToString().Trim()) != fsCHCUSTIL.ToString().Trim() ||
                    Get_Numeric(this.TXT01_CHCHASU.GetValue().ToString().Trim()) != fsCHCHASU.ToString().Trim() ||
                    this.TXT01_CHJGHWAJU.GetValue().ToString() != fsCHJGHWAJU.ToString().Trim() ||
                    this.CBH01_CHYSHWAJU.GetValue().ToString() != fsCHYSHWAJU.ToString().Trim() ||
                    this.CBH01_CHYDHWAJU.GetValue().ToString() != fsCHYDHWAJU.ToString().Trim() ||
                    Get_Numeric(Get_Date(this.DTP01_CHYSDATE.GetValue().ToString().Trim())) != Get_Numeric(fsCHYSDATE.ToString().Trim()) ||
                    Get_Numeric(this.TXT01_CHYDSEQ.GetValue().ToString()) != Get_Numeric(fsCHYDSEQ.ToString().Trim()) ||
                    Get_Numeric(this.TXT01_CHYSSEQ.GetValue().ToString()) != Get_Numeric(fsCHYSSEQ.ToString().Trim()) ||
                    this.TXT01_CHCHTANK.GetValue().ToString().Trim() != fsCHCHTANK.ToString().Trim() ||
                    this.TXT01_CHIPTANK.GetValue().ToString().Trim() != fsCHIPTANK.ToString().Trim() ||
                    Get_Numeric(this.TXT01_CHJUNG.GetValue().ToString()) != Get_Numeric(fsCHJUNG.ToString())
                    )
            {
                this.ShowMessage("TY_M_UT_6CNCK197");
                this.TXT01_CHJGHWAJU.Focus();

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
        private void UP_FieldClear(string sGUBUN)
        {
            if (sGUBUN.ToString() == "NEW")
            {
                this.TXT01_CHTKNO.SetValue("");
                this.TXT01_CHKLQTY.SetValue("");
                this.TXT01_CHJGTKNO.SetValue("");
                this.TXT01_CHJGTANK.SetValue("");
                this.TXT01_CHJGTEMP.SetValue("");
                this.TXT01_CHJGBIJUNG.SetValue("");
            }
            else
            {
                if (fsPOPUP != "POPUP")
                {
                    this.TXT01_CHTKNO.SetValue("");      // 출고순번
                }

                this.DTP01_CHIPHANG.SetValue("");    // 입항일자
                this.CBH01_CHBONSUN.SetValue("");    // 본선
                this.CBH01_CHHWAJU.SetValue("");     // 화주
                this.CBH01_CHHWAMUL.SetValue("");    // 화물
                this.TXT01_CHBLNO.SetValue("");      // BL번호
                this.TXT01_CHMSNSEQ.SetValue("");    // MSN번호
                this.TXT01_CHHSNSEQ.SetValue("");    // HSN번호
                this.DTP01_CHCUSTIL.SetValue("");    // 통관일자
                this.TXT01_CHCHASU.SetValue("");     // 통관차수
                this.TXT01_CHJGHWAJU.SetValue("");   // 재고화주
                this.TXT01_CHJGHWAJUNM.SetValue(""); // 재고화주명
                this.CBH01_CHACTHJ.SetValue("");     // 통관화주
                this.CBH01_CHYDHWAJU.SetValue("");   // 양도화주
                this.CBH01_CHYSHWAJU.SetValue("");   // 양수화주
                this.DTP01_CHYSDATE.SetValue("");    // 양수일자
                this.TXT01_CHYSSEQ.SetValue("");     // 양수순번
                this.TXT01_CHYDSEQ.SetValue("");     // 양도차수
                this.DTP01_CHSUCHIP.SetValue("");    // 수출일자
            }



            //this.TXT01_CJCUQTY.SetValue("");
            //this.TXT01_CJCHQTY.SetValue("");
            //this.TXT01_CJJEQTY.SetValue("");
            //this.TXT01_CHTANKNO.SetValue("");
            //this.TXT01_CHIPTANK.SetValue("");
            //this.CBH01_CHHISAB.SetValue("");
            //this.TXT01_CHJIQTY.SetValue("");


            //this.TXT01_CHJUNG.SetValue("");
            //this.TXT01_CHIPQTY.SetValue("");
            //this.TXT01_CHCHQTY.SetValue("");
            //this.TXT01_CHJANQTY.SetValue("");
            //this.TXT01_CHTANKNO.SetValue("");
            //this.CBH01_CHCHHJ.SetValue("");
            //this.CBH01_CHHISAB.SetValue("");
            //this.TXT01_CHJIQTY.SetValue("");
            //this.TXT01_CHJIJAN.SetValue("");
            //this.TXT01_CHIPTANK.SetValue("");
        }
        #endregion

        #region Description : 텍스트 ReadOnly
        private void UP_Set_ReadOnly(string sGUBUN)
        {
            if (sGUBUN == "NEW")
            {
                this.DTP01_CHCHULIL.SetReadOnly(false);
                this.TXT01_CHTKNO.SetReadOnly(false);
            }
            else
            {
                this.DTP01_CHCHULIL.SetReadOnly(true);
                this.TXT01_CHTKNO.SetReadOnly(true);
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
        private void FPS91_TY_S_UT_6CLD2141_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            this.DTP01_CHCHULIL.SetValue(this.FPS91_TY_S_UT_6CLD2141.GetValue("CHCHULIL").ToString());
            this.TXT01_CHTKNO.SetValue(this.FPS91_TY_S_UT_6CLD2141.GetValue("CHTKNO").ToString());

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

            if (Get_Date(this.DTP01_CHIPHANG.GetValue().ToString()) != "" && this.CBH01_CHBONSUN.GetValue().ToString() != "" &&
               this.CBH01_CHHWAJU.GetValue().ToString() != "" && this.CBH01_CHHWAMUL.GetValue().ToString() != "")
            {
                // 출고탱크
                //UP_GET_CHCHTANK();

                // 출고탱크
                UP_GET_CHIPTANK();

                SetFocus(this.TXT01_CHCHTANK);
            }
        }

        private void TXT01_CHJGHWAJU_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.F1)
            {
                UP_CALL_JEGO();

                if (Get_Date(this.DTP01_CHIPHANG.GetValue().ToString()) != "" && this.CBH01_CHBONSUN.GetValue().ToString() != "" &&
                    this.CBH01_CHHWAJU.GetValue().ToString() != "" && this.CBH01_CHHWAMUL.GetValue().ToString() != "")
                {
                    // 출고탱크
                    //UP_GET_CHCHTANK();

                    // 입고탱크
                    UP_GET_CHIPTANK();

                    SetFocus(this.TXT01_CHCHTANK);
                }
            }
        }
        #endregion

        #region Description : 출고탱크 이벤트
        private void BTN61_CHTANK_Click(object sender, EventArgs e)
        {
            TYUTGB009S popup = new TYUTGB009S(this.DTP01_CHIPHANG.GetValue().ToString(), this.CBH01_CHBONSUN.GetValue().ToString(),
                                              this.CBH01_CHHWAJU.GetValue().ToString(), this.CBH01_CHHWAMUL.GetValue().ToString());

            if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.TXT01_CHCHTANK.SetValue(popup.fsTANKNO); // 출고탱크

                // 출하장 가져오기
                UP_GET_CHCHJANG();

                SetFocus(this.TXT01_CHIPTANK);
            }
        }

        private void TXT01_CHCHTANK_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.F1)
            {
                TYUTGB009S popup = new TYUTGB009S(this.DTP01_CHIPHANG.GetValue().ToString(), this.CBH01_CHBONSUN.GetValue().ToString(),
                                                  this.CBH01_CHHWAJU.GetValue().ToString(), this.CBH01_CHHWAMUL.GetValue().ToString());

                if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    this.TXT01_CHCHTANK.SetValue(popup.fsTANKNO); // 출고탱크

                    // 출하장 가져오기
                    UP_GET_CHCHJANG();

                    SetFocus(this.TXT01_CHIPTANK);
                }
            }
        }
        #endregion

        #region Description : 출고탱크 가져오기
        private void UP_GET_CHCHTANK()
        {
            this.TXT01_CHCHTANK.SetValue("");

            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_6BAF3715",
                this.DTP01_CHIPHANG.GetValue().ToString(),       // 입항일자
                this.CBH01_CHBONSUN.GetValue().ToString(),       // 본선
                this.CBH01_CHHWAJU.GetValue().ToString(),        // 화주
                this.CBH01_CHHWAMUL.GetValue().ToString()        // 화물
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                if (dt.Rows.Count == 1)
                {
                    this.TXT01_CHCHTANK.SetValue(dt.Rows[0]["SVTANKNO"].ToString());

                    // 출하장 가져오기
                    UP_GET_CHCHJANG();
                }
            }
        }
        #endregion

        #region Description : 입고탱크 이벤트
        private void BTN61_IPTANK_Click(object sender, EventArgs e)
        {
            TYUTGB010S popup = new TYUTGB010S(this.DTP01_CHIPHANG.GetValue().ToString(), this.CBH01_CHBONSUN.GetValue().ToString(),
                                              this.CBH01_CHHWAJU.GetValue().ToString(), this.CBH01_CHHWAMUL.GetValue().ToString());

            if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.TXT01_CHIPTANK.SetValue(popup.fsTANKNO); // 출고탱크
                SetFocus(this.CBO01_CHWKTYPE);
            }
        }

        private void TXT01_CHIPTANK_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.F1)
            {
                TYUTGB010S popup = new TYUTGB010S(this.DTP01_CHIPHANG.GetValue().ToString(), this.CBH01_CHBONSUN.GetValue().ToString(),
                                              this.CBH01_CHHWAJU.GetValue().ToString(), this.CBH01_CHHWAMUL.GetValue().ToString());

                if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    this.TXT01_CHIPTANK.SetValue(popup.fsTANKNO); // 출고탱크
                    SetFocus(this.CBO01_CHWKTYPE);
                }
            }
        }
        #endregion

        #region Description : 입고탱크 가져오기
        private void UP_GET_CHIPTANK()
        {
            this.TXT01_CHIPTANK.SetValue("");

            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_6BAF3716",
                this.DTP01_CHIPHANG.GetValue().ToString(),       // 입항일자
                this.CBH01_CHBONSUN.GetValue().ToString(),       // 본선
                this.CBH01_CHHWAJU.GetValue().ToString(),        // 화주
                this.CBH01_CHHWAMUL.GetValue().ToString()        // 화물
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                if (dt.Rows.Count == 1)
                {
                    this.TXT01_CHIPTANK.SetValue(dt.Rows[0]["COTANKNO"].ToString());
                }
            }
        }
        #endregion

        #region Description : 출하장 가져오기
        private void UP_GET_CHCHJANG()
        {
            this.CBH01_CHCHJANG.SetValue("");

            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_6BAF5721",
                this.TXT01_CHCHTANK.GetValue().ToString().Trim()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                if (dt.Rows.Count == 1)
                {
                    this.CBH01_CHCHJANG.SetValue(dt.Rows[0]["TNLOCATE"].ToString());
                }
            }
        }
        #endregion

        #region Description : 지시수량 이벤트
        private void TXT01_CHJIQTY_KeyPress(object sender, KeyPressEventArgs e)
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

        #region Description : 조회 종료일자 이벤트
        private void DTP01_EDIPHANG_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Enter)
            {
                this.BTN61_INQ_Click(null, null);
            }
        }
        #endregion

        #region Description : 차량번호 이벤트
        private void BTN61_CARNO_Click(object sender, EventArgs e)
        {
            TYUTGB011S popup = new TYUTGB011S(this.TXT01_CHCARNO.GetValue().ToString());

            if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.TXT01_CHCARNO.SetValue(popup.fsCARNUMBER); // 차량번호
                SetFocus(this.TXT01_CHCARNO);
            }
        }

        private void TXT01_CHCARNO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.F1)
            {
                TYUTGB011S popup = new TYUTGB011S(this.TXT01_CHCARNO.GetValue().ToString());

                if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    this.TXT01_CHCARNO.SetValue(popup.fsCARNUMBER); // 차량번호
                    SetFocus(this.TXT01_CHCARNO);
                }
            }
        }
        #endregion

        #region Description : 입항 조회
        private void BTN62_UTTCODEHELP1_Click(object sender, EventArgs e)
        {
            TYUTGB003S popup = new TYUTGB003S();

            if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.DTP01_CHSUCHIP.SetValue(popup.fsIPHANG); // 입항일자
                this.CBH01_CHSUCHVS.SetValue(popup.fsBONSUN); // 본선명
            }
        }
        #endregion

        #region Description : 지시 조회
        private void BTN61_JISINUM_Click(object sender, EventArgs e)
        {
            TYUTGB017S popup = new TYUTGB017S();

            if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.TXT01_CHJISINUM1.SetValue(popup.fsJIYYMM); // 지시일자
                this.TXT01_CHJISINUM2.SetValue(popup.fsJISEQ);  // 지시순번
            }
        }

        private void TXT01_CHJISINUM1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.F1)
            {
                TYUTGB017S popup = new TYUTGB017S();

                if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    this.TXT01_CHJISINUM1.SetValue(popup.fsJIYYMM); // 지시일자
                    this.TXT01_CHJISINUM2.SetValue(popup.fsJISEQ);  // 지시순번
                }
            }
        }

        private void TXT01_CHJISINUM2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.F1)
            {
                TYUTGB017S popup = new TYUTGB017S();

                if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    this.TXT01_CHJISINUM1.SetValue(popup.fsJIYYMM); // 지시일자
                    this.TXT01_CHJISINUM2.SetValue(popup.fsJISEQ);  // 지시순번
                }
            }
        }
        #endregion

        #region Description : 지시자 이벤트
        private void CBH01_CHJISAB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SetFocus(this.BTN61_SAV);
            }
        }
        #endregion

        #region Description : 통관화주별 출고조회 버튼 이벤트
        private void BTN61_UTTCODEHELP6_Click(object sender, EventArgs e)
        {
            TYUTGB012S popup = new TYUTGB012S("CHULGO");

            if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.DTP01_CHCHULIL.SetValue(popup.fsCHCHULIL);
                this.TXT01_CHTKNO.SetValue(popup.fsCHTKNO);

                // 확인
                UP_RUN();

                this.SetFocus(this.TXT01_CHJGHWAJU);
            }
        }
        #endregion

        #region Description : 레이블 색깔 변경
        private void UP_Label_Color()
        {
            this.LBL51_CHCHTANK.ForeColor = System.Drawing.Color.Green;

            this.LBL51_CHIPTANK.ForeColor = System.Drawing.Color.Green;
            this.LBL51_CHWKTYPE.ForeColor = System.Drawing.Color.Green;
            this.LBL51_CHDANYI.ForeColor = System.Drawing.Color.Green;
            this.LBL51_CHCHULGB.ForeColor = System.Drawing.Color.Green;


            this.LBL51_CHCHSTR1.ForeColor = System.Drawing.Color.Green;
            this.LBL51_CHMTQTY.ForeColor = System.Drawing.Color.Green;
            this.LBL51_CHEMPTY.ForeColor = System.Drawing.Color.Green;
            this.LBL51_CHTOTAL.ForeColor = System.Drawing.Color.Green;
            this.LBL51_CHCHHASAB.ForeColor = System.Drawing.Color.Green;
        }
        #endregion

        #region Description : 클리어 버튼
        private void BTN61_UTTCLEAR_Click(object sender, EventArgs e)
        {
            string sSTDATE = string.Empty;
            string sEDDATE = string.Empty;
            string sCHTKNO1 = string.Empty;
            string sCHCARNO1 = string.Empty;
            string sCHCHULIL = string.Empty;
            string sCHTKNO = string.Empty;
            string sCHCHHASAB = string.Empty;


            sSTDATE = this.DTP01_STIPHANG.GetValue().ToString();
            sEDDATE = this.DTP01_EDIPHANG.GetValue().ToString();
            sCHTKNO1 = this.TXT01_CHTKNO1.GetValue().ToString();
            sCHCARNO1 = this.TXT01_CHCARNO1.GetValue().ToString();
            sCHCHHASAB = this.CBH01_CHCHHASAB.GetValue().ToString();

            sCHCHULIL = this.DTP01_CHCHULIL.GetValue().ToString();
            sCHTKNO = this.TXT01_CHTKNO.GetValue().ToString();

            this.Initialize_Controls("01");

            this.DTP01_STIPHANG.SetValue(sSTDATE.ToString());
            this.DTP01_EDIPHANG.SetValue(sEDDATE.ToString());
            this.TXT01_CHTKNO1.SetValue(sCHTKNO1.ToString());
            this.TXT01_CHCARNO1.SetValue(sCHCARNO1.ToString());

            this.CBH01_CHCHHASAB.SetValue(sCHCHHASAB.ToString());

            this.DTP01_CHCHULIL.SetValue(sCHCHULIL.ToString());
            this.TXT01_CHTKNO.SetValue(sCHTKNO.ToString());

            this.CBO01_CHWKTYPE.SetValue("01");
            this.CBO01_CHDANYI.SetValue("1");

            this.DTP01_CHENDIL.SetValue(sCHCHULIL.ToString());

            UP_SEARCH();
        }
        #endregion

        #region Description : 출하구분 선택 이벤튼 (2018.11.22 김종술과장 요청)
        private void CBH01_CHCHULGB_TextChanged(object sender, EventArgs e)
        {
            if (CBH01_CHCHULGB.GetValue().ToString().Length == 2)
            {
                if (CBH01_CHCHULGB.GetValue().ToString() == "05")
                {
                    this.CBO01_CHDANYI.SetValue("4");
                }
            }
        }
        #endregion
    }
}