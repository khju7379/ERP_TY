using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.HR00
{
    /// <summary>
    /// 발령사항 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2014.11.18 18:14
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_4BCH4388 : 발령사항 조회(사번별)
    ///  TY_P_HR_4BJ8K449 : 발령사항 등록
    ///  TY_P_HR_4BJ8K450 : 발령사항 수정
    ///  TY_P_HR_4BJ8L451 : 발령사항 삭제
    ///  TY_P_HR_4BJ8V452 : 발령사항 순번생성
    /// 
    ///  # 스프레드 정보 ####
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_246A2488 : 저장 작업을 실패했습니다.
    ///  TY_M_GB_23NAD871 : 저장하시겠습니까?
    ///  TY_M_GB_23NAD873 : 저장하였습니다.
    ///  TY_M_GB_2452W459 : 저장할 데이터가 없습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  ORGADD : 조직도저장
    ///  SAV : 저장
    ///  BLBSTEAM : 발령팀
    ///  BLBUSEO : 발령부서
    ///  BLJCCD : 발령직책
    ///  BLJJCD : 발령직위
    ///  BLJKCD : 발령직급
    ///  BLSABUN : 발령사번
    ///  BLSOSOK : 발령사업부
    ///  BLBUNOSEQ : 발령순번
    ///  BLBUNOYY : 발령년도
    ///  BLCODE : 발령코드
    ///  BLDATE : 발령일자
    ///  BLHOBN : 호 봉
    ///  BLKDATE1 : 발령시작일자
    ///  BLKDATE2 : 발령종료일자
    /// </summary>
    public partial class TYHRKB02C1 : TYBase
    {
        private string fsBLSABUN;
        private string fsBLBUNOYY;
        private string fsBLBUNOSEQ;
        private string fsBLDATE;
        private string fsBLCODE;
        private string fsEditGubn;

        #region Description : 폼로드 이벤트
        public TYHRKB02C1(string sBLSABUN, string sBLBUNOYY, string sBLBUNOSEQ, string sBLDATE, string sBLCODE, string sEditGubn)
        {
            InitializeComponent();

            this.fsBLSABUN = sBLSABUN;
            this.fsBLBUNOYY = sBLBUNOYY;
            this.fsBLBUNOSEQ = sBLBUNOSEQ;
            this.fsBLDATE = sBLDATE;
            this.fsBLCODE = sBLCODE;
            this.fsEditGubn = sEditGubn; //인사기본사항(I)에서 호출했는지, 발령조회(S)에서 호출했는지 구분한다.
        }

        private void TYHRKB02C1_Load(object sender, System.EventArgs e)
        {
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);

            this.CBH01_BLSABUN.SetValue(this.fsBLSABUN);

            if (string.IsNullOrEmpty(this.fsBLBUNOYY))
            {
                if (fsEditGubn.Trim() == "S")
                {
                    this.CBH01_BLSABUN.SetReadOnly(false);
                }
                this.CBH01_BLSOSOK.DummyValue = DateTime.Now.ToString("yyyyMMdd");
                this.CBH01_BLBUSEO.DummyValue = DateTime.Now.ToString("yyyyMMdd");
                this.CBH01_BLBSTEAM.DummyValue = DateTime.Now.ToString("yyyyMMdd");

                this.TXT01_BLBUNOYY.SetValue(Convert.ToString(DateTime.Now.ToString("yyyyMMdd")).Substring(0, 4));
                this.DTP01_BLDATE.SetValue(DateTime.Now.ToString("yyyyMMdd"));

                this.SetStartingFocus(this.TXT01_BLBUNOYY);
            }
            else
            {
                this.UP_FiledLock();

                this.UP_Run();

                this.SetStartingFocus(this.MTB01_BLKDATE1);
            }
        }
        #endregion

        #region Description : 종료 버튼 이벤트
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
        #endregion

        #region Description : 조직도 선택 이벤트
        private void BTN61_ORGADD_Click(object sender, EventArgs e)
        {
            TYHRKB02P1 popup = new TYHRKB02P1(this.DTP01_BLDATE.GetString().ToString());

            if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.CBH01_BLSOSOK.DummyValue = this.DTP01_BLDATE.GetString().ToString();
                this.CBH01_BLBUSEO.DummyValue = this.DTP01_BLDATE.GetString().ToString();
                this.CBH01_BLBSTEAM.DummyValue = this.DTP01_BLDATE.GetString().ToString();

                this.CBH01_BLSOSOK.SetValue(popup.fsSOSOK);
                this.CBH01_BLBUSEO.SetValue(popup.fsBUSEO);
                this.CBH01_BLBSTEAM.SetValue(popup.fsTEAM);
            }
        }
        #endregion

        #region Description : 저장 버튼 이벤트
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            bool bresult = true;

            //마지막 발령인지 체크하여 인사기본사항 UPDATE
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_4BCH4388", this.CBH01_BLSABUN.GetValue().ToString());
            DataTable dt = this.DbConnector.ExecuteDataTable();
            if (dt.Rows.Count > 0)
            {
                string sBLBUNOYY = dt.Rows[dt.Rows.Count - 1]["BLBUNOYY"].ToString();
                string sBLBUNOSEQ = dt.Rows[dt.Rows.Count - 1]["BLBUNOSEQ"].ToString();
                string sBLDATE = dt.Rows[dt.Rows.Count - 1]["BLDATE"].ToString();

                if (this.TXT01_BLBUNOYY.GetValue().ToString() == sBLBUNOYY &&
                    this.TXT01_BLBUNOSEQ.GetValue().ToString() == sBLBUNOSEQ &&
                    this.DTP01_BLDATE.GetString().ToString() == sBLDATE)
                {
                    bresult = true;
                }
                else
                {
                    bresult = false;
                }
            }


            this.DbConnector.CommandClear();

            if (string.IsNullOrEmpty(this.fsBLBUNOYY))
            {
                bresult = true;

                //발령등록
                this.DbConnector.Attach("TY_P_HR_4BJ8K449", this.TXT01_BLBUNOYY.GetValue(),
                                                            this.TXT01_BLBUNOSEQ.GetValue(),
                                                            this.CBH01_BLSABUN.GetValue(),
                                                            this.DTP01_BLDATE.GetString().ToString(),
                                                            this.CBH01_BLCODE.GetValue(),
                                                            this.MTB01_BLKDATE1.GetValue().ToString().Replace("-", "").Trim(),
                                                            this.MTB01_BLKDATE2.GetValue().ToString().Replace("-", "").Trim(),
                                                            this.CBH01_BLSOSOK.GetValue(),
                                                            this.CBH01_BLBUSEO.GetValue(),
                                                            this.CBH01_BLBSTEAM.GetValue(),
                                                            this.CBH01_BLJCCD.GetValue(),
                                                            this.CBH01_BLJKCD.GetValue(),
                                                            this.CBH01_BLJJCD.GetValue(),
                                                            this.TXT01_BLHOBN.GetValue(),
                                                            this.TXT01_BLBIGO.GetValue(),
                                                            TYUserInfo.EmpNo
                                                            );
            }
            else
            {
                this.DbConnector.Attach("TY_P_HR_4BJ8K450",this.MTB01_BLKDATE1.GetValue().ToString().Replace("-", "").Trim(),
                                                           this.MTB01_BLKDATE2.GetValue().ToString().Replace("-", "").Trim(),
                                                           this.CBH01_BLSOSOK.GetValue(),
                                                           this.CBH01_BLBUSEO.GetValue(),
                                                           this.CBH01_BLBSTEAM.GetValue(),
                                                           this.CBH01_BLJCCD.GetValue(),
                                                           this.CBH01_BLJKCD.GetValue(),
                                                           this.CBH01_BLJJCD.GetValue(),
                                                           this.TXT01_BLHOBN.GetValue(),
                                                           this.TXT01_BLBIGO.GetValue(),
                                                           TYUserInfo.EmpNo,
                                                           this.TXT01_BLBUNOYY.GetValue(),
                                                           this.TXT01_BLBUNOSEQ.GetValue(),
                                                           this.CBH01_BLSABUN.GetValue(),
                                                           this.DTP01_BLDATE.GetString().ToString(),
                                                           this.CBH01_BLCODE.GetValue()                                                          
                                                           );
            }            

            if (bresult)
            {
                //인사기본사항 수정
                this.DbConnector.Attach("TY_P_HR_4BJB5457", this.CBH01_BLJJCD.GetValue(),
                                                             this.CBH01_BLJCCD.GetValue(),
                                                             this.CBH01_BLJKCD.GetValue(),
                                                             this.TXT01_BLHOBN.GetValue(),
                                                             this.CBH01_BLSOSOK.GetValue(),
                                                             this.CBH01_BLBUSEO.GetValue(),
                                                             this.CBH01_BLBSTEAM.GetValue(),
                                                             this.CBH01_BLCODE.GetValue(),
                                                             this.DTP01_BLDATE.GetString().ToString(),
                                                             TYUserInfo.EmpNo,
                                                             this.CBH01_BLSABUN.GetValue()
                                                            );
                //구 인사기본사항 수정
                this.DbConnector.Attach("TY_P_HR_5C8HJ291", UP_Get_OldInsaCode("2", this.CBH01_BLJJCD.GetValue().ToString()),
                                                             UP_Get_OldInsaCode("3", this.CBH01_BLJCCD.GetValue().ToString()),
                                                             this.CBH01_BLJKCD.GetValue(),
                                                             this.TXT01_BLHOBN.GetValue(),
                                                             this.CBH01_BLSOSOK.GetValue(),
                                                             this.CBH01_BLBUSEO.GetValue(),
                                                             UP_Get_OldInsaCode("4", this.CBH01_BLCODE.GetValue().ToString()),
                                                             this.DTP01_BLDATE.GetString().ToString(),
                                                             this.CBH01_BLSABUN.GetValue()
                                                            );
            }

            this.DbConnector.ExecuteNonQueryList();
            this.ShowMessage("TY_M_GB_23NAD873");
        }
        #endregion

        #region Description : 저장 ProcessCheck 이벤트
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            
            //발령일자 이후 등록된 자료가 있는지 체크한다.
            //this.DbConnector.CommandClear();
            //this.DbConnector.Attach("TY_P_HR_4BJAX455", this.CBH01_BLSABUN.GetValue(), this.DTP01_BLDATE.GetString());
            //Int16 iCnt = Convert.ToInt16(this.DbConnector.ExecuteScalar());
            //if (iCnt > 0)
            //{
            //    this.ShowMessage("TY_M_HR_4BJB0456");
            //    e.Successed = false;
            //    return;
            //}

            //신규
            if (string.IsNullOrEmpty(this.fsBLBUNOYY))
            {
                //순번생성
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_4BJ8V452", this.TXT01_BLBUNOYY.GetValue().ToString());
                Int16 iSeq = Convert.ToInt16(this.DbConnector.ExecuteScalar());

                this.TXT01_BLBUNOSEQ.SetValue(Set_Fill3(iSeq.ToString()));
            }

            if (!this.ShowMessage("TY_M_GB_23NAD871"))
            {
                e.Successed = false;
                return;
            }
        }
        #endregion

        #region Description : UP_RUN 함수
        private void UP_Run()
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_4BJ9Y453", this.fsBLSABUN, this.fsBLBUNOYY, this.fsBLBUNOSEQ, this.fsBLDATE, this.fsBLCODE);
            DataSet ds = this.DbConnector.ExecuteDataSet();

            if (ds.Tables[0].Rows.Count > 0)
            {
                this.CBH01_BLSOSOK.DummyValue = ds.Tables[0].Rows[0]["BLDATE"].ToString();
                this.CBH01_BLBUSEO.DummyValue = ds.Tables[0].Rows[0]["BLDATE"].ToString();
                this.CBH01_BLBSTEAM.DummyValue = ds.Tables[0].Rows[0]["BLDATE"].ToString();

                this.CurrentDataTableRowMapping(ds.Tables[0], "01");
            }
        }
        #endregion

        #region Description : UP_FiledLock 함수
        private void UP_FiledLock()
        {
            this.TXT01_BLBUNOYY.SetReadOnly(true);
            this.CBH01_BLCODE.SetReadOnly(true);
            this.DTP01_BLDATE.SetReadOnly(true);
        }
        #endregion

    }
}
