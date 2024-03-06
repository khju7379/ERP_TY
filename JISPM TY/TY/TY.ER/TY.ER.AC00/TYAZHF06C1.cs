using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.AC00
{
    /// <summary>
    /// 소모품 예산 세목 조회 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2012.10.26 09:03
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_2AQ58859 : 소모품비 세목예산 집계 조회
    ///  TY_P_AC_2AQ9W838 : 소모품비 세목예산 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_2AQ59860 : 소모품비 세목예산 집계 조회
    ///  TY_S_GB_2AQBN848 : 소모품비 세목예산 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  INQ : 조회
    ///  J2CDAC : 계정
    ///  B2DTMK : 작성일자
    ///  J2CDDP : 팀코드
    /// </summary>
    public partial class TYAZHF06C1 : TYBase
    {
        private string fsDTMK;
        private string fsCDDP;
        private string fsCDAC;

        private string fsBudGetTabGn;  //1-소모품, 2-여비교통비 3-기타세목예산

        public string fsRt_num;
        public string fsRt_seq;
        public string fsRt_name;

        private TCodeBox _TComboHelper;
        private DataRow _SelectedDataRow;

        #region Description : 폼 로드 버튼 이벤트
        public TYAZHF06C1(string sDTMK, string sCDDP, string sCDAC)
        {
            InitializeComponent();
            this.SetPopupStyle();

            this.fsDTMK = sDTMK;  //작성일자
            this.fsCDDP = sCDDP;  //귀속부서
            this.fsCDAC = sCDAC;  //계정과목
        }

        private void TYAZHF06C1_Load(object sender, System.EventArgs e)
        {
            fsBudGetTabGn = "";

            string sReturnValue = UP_Set_BudGetTab(this.fsCDAC);
            switch (sReturnValue)
            {
                //1. 97 예산세목　삭제 
                case "00":
                    fsBudGetTabGn = "";
                    break;

                //2. 98 기타예산세목　삭제 	    
                case "11":
                case "12":
                case "13":
                case "14":
                case "15":
                case "16":
                    fsBudGetTabGn = "3";
                    break;

                //3. 98 여비교통비　예산세목　삭제 
                case "21":
                case "22":
                    fsBudGetTabGn = "2";
                    break;

                //4. 98 소모품비　예산세목　삭제 
                case "31":
                case "32":
                case "33":
                case "34":
                    fsBudGetTabGn = "1";
                    break;
            }


            this.DTP01_B2DTMK.SetValue(this.fsDTMK);
            this.CBH01_J2CDDP.DummyValue = this.DTP01_B2DTMK.GetValue();
            this.CBH01_J2CDDP.SetValue(this.fsCDDP);
            this.CBH01_J2CDAC.SetValue(this.fsCDAC);

            this.DTP01_B2DTMK.SetReadOnly(true);
            this.CBH01_J2CDDP.SetReadOnly(true);
            this.CBH01_J2CDAC.SetReadOnly(true);

            //소모품예산 
            this.CBH01_J2CDJJ.SetReadOnly(true);
            this.TXT01_J2SEQ.SetReadOnly(true);
            //여비교통비
            this.CBH01_T2CDSB.SetReadOnly(true);
            this.TXT01_T2SEQ.SetReadOnly(true);
            //기타세목
            this.TXT01_P2SEQ.SetReadOnly(true);

            //소모품예산 
            this.CBH01_J2CDJJ.SetValue("");
            this.TXT01_J2SEQ.SetValue("");
            //여비교통비
            this.CBH01_T2CDSB.SetValue("");
            this.TXT01_T2SEQ.SetValue("");
            //기타세목
            this.TXT01_P2SEQ.SetValue("");

            this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 계정과목 예산 Tab 선택 함수
        private string UP_Set_BudGetTab(string sCDAC)
        {
            string sValue = "";

            //접대비 체크
            switch (sCDAC.Substring(0, 6))
            {
                case "442120":
                case "424120":
                case "441120":
                case "441220":
                    return "01";
                case "442121":
                case "424121":
                case "441121":
                case "441221":
                    return "02";
                default:
                    sValue = "00";
                    break;
            }

            // 운영비 및 분임 토의비

            switch (sCDAC)
            {

                case "44211110":
                case "42411110":
                case "44111110":
                case "44121110":
                    return "03";
                case "44212903":
                case "42412903":
                case "44112903":
                case "44122903":
                    return "04";
                default:
                    sValue = "00";
                    break;
            }

            //기타 세목
            switch (sCDAC)
            {

                case "12200100":
                case "12200200":
                case "12200300":
                case "12200400":
                case "12200500":
                case "12200600":
                case "12200700":
                case "12200800":
                case "12200900":
                case "12210000":
                    return "11";                    
                case "42411503":
                case "44121503":
                case "44111503":
                case "44211503":
                    return "13";                    
                case "42412901":
                case "44122901":
                case "44112901":
                case "44212901":
                case "46131803":
                    return "15";                    
                case "42412803":
                case "44122803":
                case "44112803":
                case "44212803":
                    return "16";
                default:
                    if (Convert.ToDouble(sCDAC) > 52001500 &&
                        Convert.ToDouble(sCDAC) < 52001599)
                    {
                        sValue = "12";
                    }
                    else if (Convert.ToDouble(sCDAC) > 42411800 &&
                        Convert.ToDouble(sCDAC) < 42411899)
                    {
                        sValue = "14";
                    }
                    else if (Convert.ToDouble(sCDAC) > 44121800 &&
                        Convert.ToDouble(sCDAC) < 44121899)
                    {
                        sValue = "14";
                    }
                    else if (Convert.ToDouble(sCDAC) > 44111800 &&
                        Convert.ToDouble(sCDAC) < 44111899)
                    {
                        sValue = "14";
                    }
                    else if (Convert.ToDouble(sCDAC) > 44211800 &&
                        Convert.ToDouble(sCDAC) < 44211899)
                    {
                        sValue = "14";
                    }
                    if (sValue != "00") return sValue;
                    break;
            }

            //여비교통비
            switch (sCDAC)
            {
                case "42411201":
                case "44121201":
                case "44111201":
                case "44211201":
                    return "21";
                case "42411202":
                case "44121202":
                case "44111202":
                case "44211202":
                    return "22";
            }

            switch (sCDAC)
            {
                case "42413301":
                case "44123301":
                case "44113301":
                case "44213301":
                    return "31";                    
                case "42413388":
                case "44123388":
                case "44113388":
                case "44213388":
                    return "32";
            }


            /*  전산관련  --->   35 :소프트웨어개발 ,전산기기판매,소프트웨어개발외상매출 ,전산기기판매외상매출금 ,소프트웨어 매출원가
            *                     ( 41300100 : 41300200 : 11100485 ,11100486 : 42300000 ) */
            
            switch (sCDAC)
            {
                case "41300100":
                case "41300200":
                case "11100485":
                case "11100486":
                case "42300000":
                    return "35";
            }
            return sValue;
        }
        #endregion

        #region Description : 종료 버튼 이벤트
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();  
        }
        #endregion

        #region Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            if (fsBudGetTabGn == "1")  //소모품비 예산
            {
                this.FPS91_TY_S_AC_2AQ59860.Initialize();
                this.FPS91_TY_S_GB_2AQBN848.Initialize();

                this.DbConnector.CommandClear();

                this.DbConnector.Attach("TY_P_AC_2AQ58859", this.DTP01_B2DTMK.GetString().ToString().Substring(0, 4), this.CBH01_J2CDDP.GetValue(), this.CBH01_J2CDAC.GetValue());
                this.FPS91_TY_S_AC_2AQ59860.SetValue(this.DbConnector.ExecuteDataTable());

                if (this.FPS91_TY_S_AC_2AQ59860.CurrentRowCount > 0)
                {
                    //this.SpreadSumRowAdd(this.FPS91_TY_S_AC_2AQ59860, "J2CDJJNM", "예산합계", SumRowType.Sum, "J2CRAMT");
                    FPS91_TY_S_AC_2AQ59860_CellDoubleClick(null, null);
                }

                
            }
            else if (fsBudGetTabGn == "2") //여비교통비 예산
            {
                this.FPS91_TY_S_AC_2AT9M867.Initialize();
                this.FPS91_TY_S_AC_26F20892.Initialize();

                this.DbConnector.CommandClear();

                this.DbConnector.Attach("TY_P_AC_2AT9K865", this.DTP01_B2DTMK.GetString().ToString().Substring(0, 4), this.CBH01_J2CDDP.GetValue(), this.CBH01_J2CDAC.GetValue());
                this.FPS91_TY_S_AC_2AT9M867.SetValue(this.DbConnector.ExecuteDataTable());

                if (this.FPS91_TY_S_AC_2AT9M867.CurrentRowCount > 0)
                {
                   //this.SpreadSumRowAdd(this.FPS91_TY_S_AC_2AT9M867, "T2CDSBNM", "예산합계", SumRowType.Sum,"T2CRAMT");
                    FPS91_TY_S_AC_2AT9M867_CellDoubleClick(null, null);  
                }              

            }
            else if (fsBudGetTabGn == "3")  //기타세목 예산
            {
                this.FPS91_TY_S_AC_2ATC3886.Initialize();
                this.FPS91_TY_S_AC_2ATBX884.Initialize();

                this.DbConnector.CommandClear();

                this.DbConnector.Attach("TY_P_AC_2ATC1885", this.DTP01_B2DTMK.GetString().ToString().Substring(0, 4), this.CBH01_J2CDDP.GetValue(), this.CBH01_J2CDAC.GetValue());
                this.FPS91_TY_S_AC_2ATC3886.SetValue(this.DbConnector.ExecuteDataTable());

                if (this.FPS91_TY_S_AC_2ATC3886.CurrentRowCount > 0)
                {
                    //this.SpreadSumRowAdd(this.FPS91_TY_S_AC_2ATC3886, "P2SEQNM", "예산합계", SumRowType.Sum, "P2CRAMT");
                    FPS91_TY_S_AC_2ATC3886_CellDoubleClick(null, null);                
                }
                
            }

            UP_Set_TabControl();
        }
        #endregion

        #region Description : UP_Set_TabControl Tab Display 이벤트
        private void UP_Set_TabControl()
        {
            if (this.BudGetTabControl.TabPages.Contains(this.BudGetTab1))
                this.BudGetTabControl.TabPages.Remove(this.BudGetTab1);
            if (this.BudGetTabControl.TabPages.Contains(this.BudGetTab2))
                this.BudGetTabControl.TabPages.Remove(this.BudGetTab2);
            if (this.BudGetTabControl.TabPages.Contains(this.BudGetTab3))
                this.BudGetTabControl.TabPages.Remove(this.BudGetTab3);

            if (fsBudGetTabGn == "1")
            {
                this.BudGetTabControl.TabPages.Add(this.BudGetTab1);
            }
            else if (fsBudGetTabGn == "2")
            {
                this.BudGetTabControl.TabPages.Add(this.BudGetTab2);
            }
            else if (fsBudGetTabGn == "3")
            {
                this.BudGetTabControl.TabPages.Add(this.BudGetTab3);
            }
        }
        #endregion

        ////필수..시작
        #region Description : 필수 선택 조회 
        public void ConfirmEventInterface()
        {
            int row = 0;
            string sNum = "";
            string sSeq = "";
            string code = "";
            string name = "";

            if (fsBudGetTabGn == "1")
            {
                row = FPS91_TY_S_GB_2AQBN848.ActiveSheet.ActiveRowIndex;
                //예산번호
                sNum = this.FPS91_TY_S_GB_2AQBN848.GetValue(row, "J2YEAR").ToString() + this.FPS91_TY_S_GB_2AQBN848.GetValue(row, "J2CDDP").ToString() +
                             this.FPS91_TY_S_GB_2AQBN848.GetValue(row, "J2CDJJ").ToString() + this.FPS91_TY_S_GB_2AQBN848.GetValue(row, "J2SEQ").ToString();

                sSeq = this.FPS91_TY_S_GB_2AQBN848.GetValue(row, "J2SEQ").ToString();
            }
            else if (fsBudGetTabGn == "2")
            {
                row = FPS91_TY_S_AC_26F20892.ActiveSheet.ActiveRowIndex;
                sNum = this.FPS91_TY_S_AC_26F20892.GetValue(row, "T2YEAR").ToString() + this.FPS91_TY_S_AC_26F20892.GetValue(row, "T2CDDP").ToString() +
                       this.FPS91_TY_S_AC_26F20892.GetValue(row, "T2CDSB").ToString() + this.FPS91_TY_S_AC_26F20892.GetValue(row, "T2SEQ").ToString();

                sSeq = this.FPS91_TY_S_AC_26F20892.GetValue(row, "T2SEQ").ToString();
            }
            else if (fsBudGetTabGn == "3")
            {
                row = FPS91_TY_S_AC_2ATBX884.ActiveSheet.ActiveRowIndex;

                sNum = this.FPS91_TY_S_AC_2ATBX884.GetValue(row, "P2YEAR").ToString() + this.FPS91_TY_S_AC_2ATBX884.GetValue(row, "P2CDDP").ToString() +
                       this.FPS91_TY_S_AC_2ATBX884.GetValue(row, "P2SEQ").ToString();

                sSeq = this.FPS91_TY_S_AC_2ATBX884.GetValue(row, "P2SEQ").ToString();
            }


            code = sNum;
            name = sNum;

            fsRt_num = name; // 예산번호 전체

            fsRt_seq = sSeq; // 예산순번

            //if (fsBudGetTabGn == "1")
            //{
            //    this._SelectedDataRow = this.FPS91_TY_S_GB_2AQBN848.GetDataRow(row);
            //}
            //else if (fsBudGetTabGn == "2")
            //{
            //    this._SelectedDataRow = this.FPS91_TY_S_AC_26F20892.GetDataRow(row);
            //}
            //else if (fsBudGetTabGn == "3")
            //{
            //    this._SelectedDataRow = this.FPS91_TY_S_AC_2ATBX884.GetDataRow(row);
            //}

            //if (this._TComboHelper != null)
            //{
            //    this._TComboHelper.SetValue(code, name);
            //    this._TComboHelper.BindedDataRow = _SelectedDataRow;
            //}

            this.DialogResult = System.Windows.Forms.DialogResult.OK;

            this.Close();
        }




        #endregion
        //필수...끝

        // 소모품 예산
        #region Description : FPS91_TY_S_AC_2AQ59860_CellDoubleClick 이벤트
        private void FPS91_TY_S_AC_2AQ59860_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            this.FPS91_TY_S_GB_2AQBN848.Initialize();

            this.CBH01_J2CDJJ.SetValue(this.FPS91_TY_S_AC_2AQ59860.GetValue("J2CDJJ").ToString());
            this.TXT01_J2SEQ.SetValue(this.FPS91_TY_S_AC_2AQ59860.GetValue("J2SEQ").ToString());

            fsRt_name = this.FPS91_TY_S_AC_2AQ59860.GetValue("J2SEQNM").ToString();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_2AQ9W838", this.FPS91_TY_S_AC_2AQ59860.GetValue("J2YEAR").ToString(),
                                                        this.FPS91_TY_S_AC_2AQ59860.GetValue("J2CDDP").ToString(),
                                                        this.FPS91_TY_S_AC_2AQ59860.GetValue("J2CDAC").ToString(),
                                                        this.FPS91_TY_S_AC_2AQ59860.GetValue("J2CDJJ").ToString(),
                                                        this.FPS91_TY_S_AC_2AQ59860.GetValue("J2SEQ").ToString());
            this.FPS91_TY_S_GB_2AQBN848.SetValue(this.DbConnector.ExecuteDataTable());
        }
        #endregion

        #region Description : FPS91_TY_S_GB_2AQBN848_CellDoubleClick 이벤트
        private void FPS91_TY_S_GB_2AQBN848_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (e.ColumnHeader)
                return;
            this.ConfirmEventInterface();
        }
        #endregion

        // 여비교통비
        #region Description : FPS91_TY_S_AC_2AT9M867_CellDoubleClick 이벤트
        private void FPS91_TY_S_AC_2AT9M867_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            this.FPS91_TY_S_AC_26F20892.Initialize();

            this.CBH01_T2CDSB.SetValue(this.FPS91_TY_S_AC_2AT9M867.GetValue("T2CDSB").ToString());
            this.TXT01_T2SEQ.SetValue(this.FPS91_TY_S_AC_2AT9M867.GetValue("T2SEQ").ToString());

            fsRt_name = this.FPS91_TY_S_AC_2AT9M867.GetValue("T2CDSBNM").ToString();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_26F31898", this.FPS91_TY_S_AC_2AT9M867.GetValue("T2YEAR").ToString(),
                                                        this.FPS91_TY_S_AC_2AT9M867.GetValue("T2CDDP").ToString(),
                                                        this.FPS91_TY_S_AC_2AT9M867.GetValue("T2CDAC").ToString(),
                                                        this.FPS91_TY_S_AC_2AT9M867.GetValue("T2CDSB").ToString(),
                                                        this.FPS91_TY_S_AC_2AT9M867.GetValue("T2SEQ").ToString());
            this.FPS91_TY_S_AC_26F20892.SetValue(this.DbConnector.ExecuteDataTable());
        }
        #endregion

        #region Description : FPS91_TY_S_AC_26F20892_CellDoubleClick 이벤트
        private void FPS91_TY_S_AC_26F20892_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (e.ColumnHeader)
                return;
            this.ConfirmEventInterface();
            
        }
        #endregion

        // 기타 세목예산
        #region Description : FPS91_TY_S_AC_2ATC3886_CellDoubleClick 이벤트
        private void FPS91_TY_S_AC_2ATC3886_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            this.FPS91_TY_S_AC_2ATBX884.Initialize();

            this.TXT01_P2SEQ.SetValue(this.FPS91_TY_S_AC_2ATC3886.GetValue("P2SEQ").ToString());

            fsRt_name = this.FPS91_TY_S_AC_2ATC3886.GetValue("P2SEQNM").ToString();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_2ATBW882", this.FPS91_TY_S_AC_2ATC3886.GetValue("P2YEAR").ToString(),
                                                        this.FPS91_TY_S_AC_2ATC3886.GetValue("P2CDDP").ToString(),
                                                        this.FPS91_TY_S_AC_2ATC3886.GetValue("P2CDAC").ToString(),
                                                        this.FPS91_TY_S_AC_2ATC3886.GetValue("P2SEQ").ToString());
            this.FPS91_TY_S_AC_2ATBX884.SetValue(this.DbConnector.ExecuteDataTable());
        }
        #endregion

        #region Description : FPS91_TY_S_AC_2ATBX884_CellDoubleClick 이벤트
        private void FPS91_TY_S_AC_2ATBX884_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (e.ColumnHeader)
                return;
            this.ConfirmEventInterface();
        }
        #endregion
    }
}
