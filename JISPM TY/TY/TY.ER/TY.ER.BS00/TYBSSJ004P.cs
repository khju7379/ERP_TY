using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls; 
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.BS00
{
    /// <summary>
    /// 당기실적 투자.수선관리 등록 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2017.09.21 15:07
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_79LF5649 : 당기실적 투자.수선관리 등록
    /// 
    ///  # 스프레드 정보 ####
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_GB_23NAD871 : 저장하시겠습니까?
    ///  TY_M_GB_23NAD873 : 저장하였습니다.
    ///  TY_M_GB_2452W459 : 저장할 데이터가 없습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  SAV : 저장
    ///  BCJASETGN : 자산코드
    ///  BCJYYMM : 실적생성년월
    ///  BCJCDAC : 계정과목
    ///  BCJDPAC : 귀속부서
    ///  BCJFORM : 양식구분
    ///  BCJITEM : 항목코드
    ///  BCJMEMO : 수정사유
    ///  BCJMONAMT01 : 1월
    ///  BCJMONAMT02 : 2월
    ///  BCJMONAMT03 : 3월
    ///  BCJMONAMT04 : 4월
    ///  BCJMONAMT05 : 5월
    ///  BCJMONAMT06 : 6월
    ///  BCJMONAMT07 : 7월
    ///  BCJMONAMT08 : 8월
    ///  BCJMONAMT09 : 9월
    ///  BCJMONAMT10 : 10월
    ///  BCJMONAMT11 : 11월
    ///  BCJMONAMT12 : 12월
    ///  BCJMONTOTAL : 합계
    ///  BCJTYPE : 유형코드
    ///  BCJYEAR : 년도
    /// </summary>
    public partial class TYBSSJ004P : TYBase
    {
        private string fsBCJYYMM;

        #region  Description : 폼 로드 이벤트
        public TYBSSJ004P(string sBCJYYMM)
        {
            InitializeComponent();

            this.SetPopupStyle();

            fsBCJYYMM = sBCJYYMM;
        }

        private void TYBSSJ004P_Load(object sender, System.EventArgs e)
        {
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);

            this.CBH01_BCJYYMM.SetValue(fsBCJYYMM);
            this.CBO01_BCJFORM.SetValue("PR");
            this.CBO01_BCJFORM.SetReadOnly(true);

            this.TXT01_BCJYEAR.SetValue(fsBCJYYMM.Substring(0,4));
            
            CBH01_BCJDPAC.DummyValue = fsBCJYYMM.Substring(0, 4) + "0101";
            CBH01_BCJITEM.DummyValue = fsBCJYYMM.Substring(0, 4);

            CBH01_BCJADAC.CodeText.Text = "자동입력";

            this.SetStartingFocus(CBH01_BCJDPAC.CodeText);

        }
        #endregion

        #region  Description : 저장 버튼 이벤트
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_79MGQ660",  CBH01_BCJYYMM.GetValue().ToString(),
                                                         CBO01_BCJFORM.GetValue().ToString(),
                                                         TXT01_BCJYEAR.GetValue().ToString(),
                                                         CBH01_BCJDPAC.GetValue().ToString(),
                                                         CBH01_BCJADAC.GetValue().ToString(),
                                                         CBH01_BCJCDAC.GetValue().ToString(),
                                                         CBH01_BCJITEM.GetValue().ToString(),
                                                         TXT01_BCJLNGUBN.GetValue().ToString(),
                                                         "N",
                                                         TXT01_BCJMONAMT01.GetValue().ToString(),
                                                         TXT01_BCJMONAMT02.GetValue().ToString(),
                                                         TXT01_BCJMONAMT03.GetValue().ToString(),
                                                         TXT01_BCJMONAMT04.GetValue().ToString(),
                                                         TXT01_BCJMONAMT05.GetValue().ToString(),
                                                         TXT01_BCJMONAMT06.GetValue().ToString(),
                                                         TXT01_BCJMONAMT07.GetValue().ToString(),
                                                         TXT01_BCJMONAMT08.GetValue().ToString(),
                                                         TXT01_BCJMONAMT09.GetValue().ToString(),
                                                         TXT01_BCJMONAMT10.GetValue().ToString(),
                                                         TXT01_BCJMONAMT11.GetValue().ToString(),
                                                         TXT01_BCJMONAMT12.GetValue().ToString(),
                                                         TXT01_BCJMONTOTAL.GetValue().ToString(),
                                                         TYUserInfo.EmpNo
                                                         );
            this.DbConnector.ExecuteTranQuery();

            this.ShowMessage("TY_M_GB_23NAD873");

            this.BTN61_CLO_Click(null, null);

        }

        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            Int16 iCnt = 0;
           
            //자료중복체크
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_79MHQ663", CBH01_BCJYYMM.GetValue().ToString(),
                                                         CBO01_BCJFORM.GetValue().ToString(),
                                                         TXT01_BCJYEAR.GetValue().ToString(),
                                                         CBH01_BCJDPAC.GetValue().ToString(),
                                                         CBH01_BCJADAC.GetValue().ToString(),
                                                         CBH01_BCJCDAC.GetValue().ToString(),
                                                         CBH01_BCJITEM.GetValue().ToString());
            iCnt = Convert.ToInt16(this.DbConnector.ExecuteScalar());

            if (iCnt > 0)
            {
                this.ShowCustomMessage("동일한자료가 존재합니다. 등록할수 없습니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                e.Successed = false;
                return;
            }          

            //계정과목에 따른 영업외손익 항목코드가 맞는지 확인
            if (CBH01_BCJITEM.GetValue().ToString() != "")
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_79MHW664", TXT01_BCJYEAR.GetValue().ToString(),
                                                             CBH01_BCJADAC.GetValue().ToString(),
                                                             CBH01_BCJCDAC.GetValue().ToString(),
                                                             CBH01_BCJITEM.GetValue().ToString());
                iCnt = Convert.ToInt16(this.DbConnector.ExecuteScalar());

                if (iCnt <= 0)
                {
                    CBH01_BCJITEM.CodeText.Focus();
                    this.ShowCustomMessage("항목코드를 확인하세요!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    e.Successed = false;
                    return;
                }
            }
            else
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_76GAJ833", TXT01_BCJYEAR.GetValue().ToString(),
                                                             CBH01_BCJADAC.GetValue().ToString(),
                                                             CBH01_BCJCDAC.GetValue().ToString()                                                             
                                                             );
                DataTable dtList = this.DbConnector.ExecuteDataTable();

                if (dtList.Rows.Count > 0)
                {
                    CBH01_BCJITEM.CodeText.Focus();
                    this.ShowCustomMessage("항목코드를 확인하세요!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    e.Successed = false;
                    return;
                }
            }

            //하위항목라인이 있는지 없는지 체크
            if (CBH01_BCJITEM.GetValue().ToString() != "")
            {
                //하위항목이 없는경우
                TXT01_BCJLNGUBN.SetValue("2");
            }
            else
            {
                //하위항목이 있는경우
                TXT01_BCJLNGUBN.SetValue("1");
            }           

            //금액 재계산
            double dTotalAmount = 0;

            dTotalAmount = Convert.ToDouble(Get_Numeric(TXT01_BCJMONAMT01.GetValue().ToString())) +
                            Convert.ToDouble(Get_Numeric(TXT01_BCJMONAMT02.GetValue().ToString())) +
                            Convert.ToDouble(Get_Numeric(TXT01_BCJMONAMT03.GetValue().ToString())) +
                            Convert.ToDouble(Get_Numeric(TXT01_BCJMONAMT04.GetValue().ToString())) +
                            Convert.ToDouble(Get_Numeric(TXT01_BCJMONAMT05.GetValue().ToString())) +
                            Convert.ToDouble(Get_Numeric(TXT01_BCJMONAMT06.GetValue().ToString())) +
                            Convert.ToDouble(Get_Numeric(TXT01_BCJMONAMT07.GetValue().ToString())) +
                            Convert.ToDouble(Get_Numeric(TXT01_BCJMONAMT08.GetValue().ToString())) +
                            Convert.ToDouble(Get_Numeric(TXT01_BCJMONAMT09.GetValue().ToString())) +
                            Convert.ToDouble(Get_Numeric(TXT01_BCJMONAMT10.GetValue().ToString())) +
                            Convert.ToDouble(Get_Numeric(TXT01_BCJMONAMT11.GetValue().ToString())) +
                            Convert.ToDouble(Get_Numeric(TXT01_BCJMONAMT12.GetValue().ToString()));

            TXT01_BCJMONTOTAL.SetValue(dTotalAmount.ToString());

            if (!this.ShowMessage("TY_M_GB_23NAD871"))
            {
                e.Successed = false;
                return;
            }           

        }
        #endregion

        #region  Description : 월 금액 합계 계산 함수
        private void UP_Set_MONAMTHAP()
        {
            double dTotalAmount = 0;

            dTotalAmount = Convert.ToDouble(Get_Numeric(TXT01_BCJMONAMT01.GetValue().ToString())) +
                            Convert.ToDouble(Get_Numeric(TXT01_BCJMONAMT02.GetValue().ToString())) +
                            Convert.ToDouble(Get_Numeric(TXT01_BCJMONAMT03.GetValue().ToString())) +
                            Convert.ToDouble(Get_Numeric(TXT01_BCJMONAMT04.GetValue().ToString())) +
                            Convert.ToDouble(Get_Numeric(TXT01_BCJMONAMT05.GetValue().ToString())) +
                            Convert.ToDouble(Get_Numeric(TXT01_BCJMONAMT06.GetValue().ToString())) +
                            Convert.ToDouble(Get_Numeric(TXT01_BCJMONAMT07.GetValue().ToString())) +
                            Convert.ToDouble(Get_Numeric(TXT01_BCJMONAMT08.GetValue().ToString())) +
                            Convert.ToDouble(Get_Numeric(TXT01_BCJMONAMT09.GetValue().ToString())) +
                            Convert.ToDouble(Get_Numeric(TXT01_BCJMONAMT10.GetValue().ToString())) +
                            Convert.ToDouble(Get_Numeric(TXT01_BCJMONAMT11.GetValue().ToString())) +
                            Convert.ToDouble(Get_Numeric(TXT01_BCJMONAMT12.GetValue().ToString()));

            TXT01_BCJMONTOTAL.SetValue(dTotalAmount.ToString());
        }
        #endregion

        #region  Description : CBH01_BCJDPAC_CodeBoxDataBinded 이벤트
        private void CBH01_BCJDPAC_CodeBoxDataBinded(object sender, EventArgs e)
        {
            
        }

        private void CBH01_BCJDPAC_Leave(object sender, EventArgs e)
        {
            
        }
        #endregion
       

        #region  Description : CBH01_BCJASETGN_CodeBoxDataBinded 이벤트
        private void CBH01_BCJASETGN_CodeBoxDataBinded(object sender, EventArgs e)
        {
            
        }
        #endregion       

        #region  Description : Leave 이벤트
        private void TXT01_BCJMONAMT01_Leave(object sender, EventArgs e)
        {
            UP_Set_MONAMTHAP();
        }

        private void TXT01_BCJMONAMT02_Leave(object sender, EventArgs e)
        {
            UP_Set_MONAMTHAP();
        }

        private void TXT01_BCJMONAMT03_Leave(object sender, EventArgs e)
        {
            UP_Set_MONAMTHAP();
        }

        private void TXT01_BCJMONAMT04_Leave(object sender, EventArgs e)
        {
            UP_Set_MONAMTHAP();
        }

        private void TXT01_BCJMONAMT05_Leave(object sender, EventArgs e)
        {
            UP_Set_MONAMTHAP();
        }

        private void TXT01_BCJMONAMT06_Leave(object sender, EventArgs e)
        {
            UP_Set_MONAMTHAP();
        }

        private void TXT01_BCJMONAMT07_Leave(object sender, EventArgs e)
        {
            UP_Set_MONAMTHAP();
        }

        private void TXT01_BCJMONAMT08_Leave(object sender, EventArgs e)
        {
            UP_Set_MONAMTHAP();
        }

        private void TXT01_BCJMONAMT09_Leave(object sender, EventArgs e)
        {
            UP_Set_MONAMTHAP();
        }

        private void TXT01_BCJMONAMT10_Leave(object sender, EventArgs e)
        {
            UP_Set_MONAMTHAP();
        }

        private void TXT01_BCJMONAMT11_Leave(object sender, EventArgs e)
        {
            UP_Set_MONAMTHAP();
        }

        private void TXT01_BCJMONAMT12_Leave(object sender, EventArgs e)
        {
            UP_Set_MONAMTHAP();
        }
        #endregion

        #region  Description : KeyPress 이벤트
        private void TXT01_BCJMONAMT12_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                this.BTN61_SAV.Focus();
            }
        }      
        #endregion

        #region  Description : CBH01_BCJCDAC_CodeBoxDataBinded 이벤트
        private void CBH01_BCJCDAC_CodeBoxDataBinded(object sender, EventArgs e)
        {
            if (CBH01_BCJCDAC.GetValue().ToString().Length >= 8)
            {
                CBH01_BCJADAC.SetValue(CBH01_BCJCDAC.GetValue().ToString().Substring(0, 6) + "00");

                CBH01_BCJITEM.DummyValue = fsBCJYYMM.Substring(0, 4) + CBH01_BCJCDAC.GetValue().ToString();
            }
        }
        #endregion

        #region  Description : 닫기 버튼 이벤트
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
        #endregion





    }
}
