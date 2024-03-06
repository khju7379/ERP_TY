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
    ///  BVJASETGN : 자산코드
    ///  BVJYYMM : 실적생성년월
    ///  BVJCDAC : 계정과목
    ///  BVJDPAC : 귀속부서
    ///  BVJFORM : 양식구분
    ///  BVJITEM : 항목코드
    ///  BVJMEMO : 수정사유
    ///  BVJMONAMT01 : 1월
    ///  BVJMONAMT02 : 2월
    ///  BVJMONAMT03 : 3월
    ///  BVJMONAMT04 : 4월
    ///  BVJMONAMT05 : 5월
    ///  BVJMONAMT06 : 6월
    ///  BVJMONAMT07 : 7월
    ///  BVJMONAMT08 : 8월
    ///  BVJMONAMT09 : 9월
    ///  BVJMONAMT10 : 10월
    ///  BVJMONAMT11 : 11월
    ///  BVJMONAMT12 : 12월
    ///  BVJMONTOTAL : 합계
    ///  BVJTYPE : 유형코드
    ///  BVJYEAR : 년도
    /// </summary>
    public partial class TYBSSJ003P : TYBase
    {
        private string fsBVJYYMM;
        private string fsBVJDPAC;

        #region  Description : 폼 로드 이벤트
        public TYBSSJ003P(string sBVJYYMM, string sBVJDPAC)
        {
            InitializeComponent();

            this.SetPopupStyle();

            fsBVJYYMM = sBVJYYMM;
            fsBVJDPAC = sBVJDPAC;
        }

        private void TYBSSJ003P_Load(object sender, System.EventArgs e)
        {
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);

            this.CBH01_BVJYYMM.SetValue(fsBVJYYMM);
            
            this.TXT01_BVJYEAR.SetValue(fsBVJYYMM.Substring(0,4));

            CBH01_BVJASETGN.DummyValue = fsBVJYYMM.Substring(0, 4) + "TA";
            CBH01_BVJDPAC.DummyValue = fsBVJYYMM.Substring(0, 4) + "0101";

            this.CBH01_BVJDPAC.SetValue(fsBVJDPAC);

            CBH01_BVJCDAC.CodeText.Text = "자동입력";

            this.SetStartingFocus(CBH01_BVJDPAC.CodeText);

        }
        #endregion

        #region  Description : 저장 버튼 이벤트
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_79LF5649",  CBH01_BVJYYMM.GetValue().ToString(),
                                                         TXT01_BVJFORM.GetValue().ToString(),
                                                         TXT01_BVJYEAR.GetValue().ToString(),
                                                         CBH01_BVJDPAC.GetValue().ToString(),
                                                         CBO01_BVJTYPE.GetValue().ToString(),
                                                         CBH01_BVJASETGN.GetValue().ToString(),
                                                         CBH01_BVJCDAC.GetValue().ToString(),
                                                         CBH01_BVJYYMM.GetValue().ToString(),
                                                         TXT01_BVJITEM.GetValue().ToString(),
                                                         TXT01_BVJITEMNAME.GetValue().ToString(),
                                                         "N",
                                                         TXT01_BVJMONAMT01.GetValue().ToString(),  
                                                         TXT01_BVJMONAMT02.GetValue().ToString(),  
                                                         TXT01_BVJMONAMT03.GetValue().ToString(),  
                                                         TXT01_BVJMONAMT04.GetValue().ToString(),  
                                                         TXT01_BVJMONAMT05.GetValue().ToString(),  
                                                         TXT01_BVJMONAMT06.GetValue().ToString(),  
                                                         TXT01_BVJMONAMT07.GetValue().ToString(),  
                                                         TXT01_BVJMONAMT08.GetValue().ToString(),  
                                                         TXT01_BVJMONAMT09.GetValue().ToString(),  
                                                         TXT01_BVJMONAMT10.GetValue().ToString(),  
                                                         TXT01_BVJMONAMT11.GetValue().ToString(),  
                                                         TXT01_BVJMONAMT12.GetValue().ToString(),  
                                                         TXT01_BVJMONTOTAL.GetValue().ToString(),  
                                                         TXT01_BVJMEMO.GetValue().ToString(),  
                                                         TYUserInfo.EmpNo
                                                         );
            this.DbConnector.ExecuteTranQuery();

            this.ShowMessage("TY_M_GB_23NAD873");

            this.BTN61_CLO_Click(null, null);

        }

        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            if (CBO01_BVJTYPE.GetValue().ToString() == "A") //투자
            {
                this.TXT01_BVJFORM.SetValue("IN");
            }
            else
            {
                //수선
                this.TXT01_BVJFORM.SetValue("RE");
            }

            //항목코드 가져오기
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_79MDF656", CBH01_BVJYYMM.GetValue().ToString(),
                                                         TXT01_BVJFORM.GetValue().ToString(),
                                                         TXT01_BVJYEAR.GetValue().ToString(),
                                                         CBH01_BVJDPAC.GetValue().ToString(),
                                                         CBO01_BVJTYPE.GetValue().ToString(),
                                                         CBH01_BVJASETGN.GetValue().ToString(),
                                                         CBH01_BVJCDAC.GetValue().ToString()
                                                         );
            DataTable dt = this.DbConnector.ExecuteDataTable();
            if (dt.Rows.Count > 0)
            {
                TXT01_BVJITEM.SetValue(dt.Rows[0][0].ToString());
            }

            //계정과목 확인
            UP_Set_CDACCode();

            //금액 재계산
            double dTotalAmount = 0;

            dTotalAmount = Convert.ToDouble(Get_Numeric(TXT01_BVJMONAMT01.GetValue().ToString())) +
                            Convert.ToDouble(Get_Numeric(TXT01_BVJMONAMT02.GetValue().ToString())) +
                            Convert.ToDouble(Get_Numeric(TXT01_BVJMONAMT03.GetValue().ToString())) +
                            Convert.ToDouble(Get_Numeric(TXT01_BVJMONAMT04.GetValue().ToString())) +
                            Convert.ToDouble(Get_Numeric(TXT01_BVJMONAMT05.GetValue().ToString())) +
                            Convert.ToDouble(Get_Numeric(TXT01_BVJMONAMT06.GetValue().ToString())) +
                            Convert.ToDouble(Get_Numeric(TXT01_BVJMONAMT07.GetValue().ToString())) +
                            Convert.ToDouble(Get_Numeric(TXT01_BVJMONAMT08.GetValue().ToString())) +
                            Convert.ToDouble(Get_Numeric(TXT01_BVJMONAMT09.GetValue().ToString())) +
                            Convert.ToDouble(Get_Numeric(TXT01_BVJMONAMT10.GetValue().ToString())) +
                            Convert.ToDouble(Get_Numeric(TXT01_BVJMONAMT11.GetValue().ToString())) +
                            Convert.ToDouble(Get_Numeric(TXT01_BVJMONAMT12.GetValue().ToString()));

            TXT01_BVJMONTOTAL.SetValue(dTotalAmount.ToString());

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

            dTotalAmount = Convert.ToDouble(Get_Numeric(TXT01_BVJMONAMT01.GetValue().ToString())) +
                            Convert.ToDouble(Get_Numeric(TXT01_BVJMONAMT02.GetValue().ToString())) +
                            Convert.ToDouble(Get_Numeric(TXT01_BVJMONAMT03.GetValue().ToString())) +
                            Convert.ToDouble(Get_Numeric(TXT01_BVJMONAMT04.GetValue().ToString())) +
                            Convert.ToDouble(Get_Numeric(TXT01_BVJMONAMT05.GetValue().ToString())) +
                            Convert.ToDouble(Get_Numeric(TXT01_BVJMONAMT06.GetValue().ToString())) +
                            Convert.ToDouble(Get_Numeric(TXT01_BVJMONAMT07.GetValue().ToString())) +
                            Convert.ToDouble(Get_Numeric(TXT01_BVJMONAMT08.GetValue().ToString())) +
                            Convert.ToDouble(Get_Numeric(TXT01_BVJMONAMT09.GetValue().ToString())) +
                            Convert.ToDouble(Get_Numeric(TXT01_BVJMONAMT10.GetValue().ToString())) +
                            Convert.ToDouble(Get_Numeric(TXT01_BVJMONAMT11.GetValue().ToString())) +
                            Convert.ToDouble(Get_Numeric(TXT01_BVJMONAMT12.GetValue().ToString()));

            TXT01_BVJMONTOTAL.SetValue(dTotalAmount.ToString());
        }
        #endregion

        #region  Description : CBH01_BVJDPAC_CodeBoxDataBinded 이벤트
        private void CBH01_BVJDPAC_CodeBoxDataBinded(object sender, EventArgs e)
        {
            UP_Set_CDACCode();
        }

        private void CBH01_BVJDPAC_Leave(object sender, EventArgs e)
        {
            UP_Set_CDACCode();
        }
        #endregion

        #region  Description : 자산유형 따른 계정과목 셋팅 이벤트
        private void UP_Set_CDACCode()
        {
            if (CBH01_BVJASETGN.GetValue().ToString().Length >= 2)
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_79M9D650", this.CBH01_BVJYYMM.GetValue().ToString().Substring(0, 4) + "TA", CBH01_BVJASETGN.GetValue().ToString(), "");
                DataTable dt = this.DbConnector.ExecuteDataTable();

                if (CBO01_BVJTYPE.GetValue().ToString() == "A") //투자
                {
                    this.CBH01_BVJCDAC.SetValue(dt.Rows[0]["CDITEM1"].ToString());
                }
                else  //수선
                {
                    string sDpac = CBH01_BVJDPAC.GetValue().ToString();
                    string sHdac = string.Empty;

                    switch (sDpac)
                    {
                        case "A10000":
                        case "A80000":
                        case "A90000":
                        case "C10000":
                            sHdac = "442";
                            break;
                        case "S10000":
                        case "T10000":
                            sHdac = "424";
                            break;
                        case "S40000":
                        case "T40000":
                            sHdac = "441";
                            break;
                        default:
                            sHdac = "";
                            break;
                    }

                    if (sHdac != "")
                    {
                        this.CBH01_BVJCDAC.SetValue(sHdac + dt.Rows[0]["CDITEM2"].ToString());
                    }
                    else
                    {
                        this.CBH01_BVJCDAC.SetValue("");
                    }
                }
            }

        }
        #endregion

        #region  Description : CBH01_BVJASETGN_CodeBoxDataBinded 이벤트
        private void CBO01_BVJTYPE_SelectedIndexChanged(object sender, EventArgs e)
        {
            UP_Set_CDACCode();
        }
        #endregion

        #region  Description : CBH01_BVJASETGN_CodeBoxDataBinded 이벤트
        private void CBH01_BVJASETGN_CodeBoxDataBinded(object sender, EventArgs e)
        {
            UP_Set_CDACCode();
        }
        #endregion

        #region  Description : 닫기 버튼 이벤트
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
        #endregion

        #region  Description : Leave 이벤트
        private void TXT01_BVJMONAMT01_Leave(object sender, EventArgs e)
        {
            UP_Set_MONAMTHAP();
        }

        private void TXT01_BVJMONAMT02_Leave(object sender, EventArgs e)
        {
            UP_Set_MONAMTHAP();
        }

        private void TXT01_BVJMONAMT03_Leave(object sender, EventArgs e)
        {
            UP_Set_MONAMTHAP();
        }

        private void TXT01_BVJMONAMT04_Leave(object sender, EventArgs e)
        {
            UP_Set_MONAMTHAP();
        }

        private void TXT01_BVJMONAMT05_Leave(object sender, EventArgs e)
        {
            UP_Set_MONAMTHAP();
        }

        private void TXT01_BVJMONAMT06_Leave(object sender, EventArgs e)
        {
            UP_Set_MONAMTHAP();
        }

        private void TXT01_BVJMONAMT07_Leave(object sender, EventArgs e)
        {
            UP_Set_MONAMTHAP();
        }

        private void TXT01_BVJMONAMT08_Leave(object sender, EventArgs e)
        {
            UP_Set_MONAMTHAP();
        }

        private void TXT01_BVJMONAMT09_Leave(object sender, EventArgs e)
        {
            UP_Set_MONAMTHAP();
        }

        private void TXT01_BVJMONAMT10_Leave(object sender, EventArgs e)
        {
            UP_Set_MONAMTHAP();
        }

        private void TXT01_BVJMONAMT11_Leave(object sender, EventArgs e)
        {
            UP_Set_MONAMTHAP();
        }

        private void TXT01_BVJMONAMT12_Leave(object sender, EventArgs e)
        {
            UP_Set_MONAMTHAP();
        }
        #endregion

        #region  Description : KeyPress 이벤트
        private void TXT01_BVJITEMNAME_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                this.TXT01_BVJMONAMT01.Focus();
            }
        }

        private void TXT01_BVJMONAMT12_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                this.TXT01_BVJMEMO.Focus();
            }
        }
        private void TXT01_BVJMEMO_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                this.BTN61_SAV.Focus();
            }
        }
        #endregion

      

      


    }
}
