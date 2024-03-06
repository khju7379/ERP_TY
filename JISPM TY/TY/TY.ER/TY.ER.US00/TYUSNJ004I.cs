using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using TY.Service.Library;

namespace TY.ER.US00
{
    /// <summary>
    /// 일별 작업현황관리 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2019.04.10 08:47
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_US_949GF299 : 항운노조 일별작업현황 등록
    ///  TY_P_US_949GG300 : 항운노조 일별작업현황 수정
    ///  TY_P_US_949GL303 : 항운노조 일별작업현황 확인
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
    ///  SAV : 저장
    ///  HIGOKJONG : 곡　　종
    ///  HDYYMM : 항　　차
    ///  HIWKDATE : 작업일
    ///  HIAMT : 총금액
    ///  HIBIGO : 비고
    ///  HIDANGA : 단가
    ///  HIEDTIME : 종료시간
    ///  HIJYTIME : 적용시간
    ///  HIJYYYMM : 적용년월
    ///  HISEQ : 번호
    ///  HISTTIME : 시작시간
    ///  HIWKGUBUN : 작업구분
    ///  HIWKMAN : 작업인원
    ///  HIWKQTY : 작업량
    /// </summary>
    public partial class TYUSNJ004I : TYBase
    {
        private string  fsHDYYMM;

        #region  Description : 폼 로드 이벤트
        public TYUSNJ004I(string sHDYYMM)
        {
            InitializeComponent();

            this.SetPopupStyle();

            fsHDYYMM = sHDYYMM;
        }

        private void TYUSNJ004I_Load(object sender, System.EventArgs e)
        {
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);

            if (string.IsNullOrEmpty(this.fsHDYYMM))
            {
                this.DTP01_HDYYMM.SetValue(DateTime.Now.ToString("yyyy-MM"));

                this.SetStartingFocus(DTP01_HDYYMM);
            }
            else
            {
                this.DTP01_HDYYMM.SetValue(this.fsHDYYMM);

                this.DTP01_HDYYMM.SetReadOnly(true);

                // 확인
                UP_DataBinding();
            }
        }
        #endregion

        #region  Description : 데이타 바인딩 이벤트
        private void UP_DataBinding()
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_US_96EHM854", this.fsHDYYMM);
            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.CurrentDataTableRowMapping(dt, "01");

                this.DTP01_HDYYMM.SetReadOnly(true);

                this.SetStartingFocus(this.TXT01_HDJJAKUP);
            }
        }
        #endregion

        #region  Description : 저장 버튼 이벤트
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();
            if (string.IsNullOrEmpty(this.fsHDYYMM))
            {
                this.DbConnector.Attach("TY_P_US_96H8D855", Get_Date(this.DTP01_HDYYMM.GetValue().ToString()),
                                                            Get_Numeric(this.TXT01_HDJJAKUP.GetValue().ToString()),
                                                            Get_Numeric(this.TXT01_HDBJAKUP.GetValue().ToString()),
                                                            Get_Numeric(this.TXT01_HDYJAKUP.GetValue().ToString()),
                                                            Get_Numeric(this.TXT01_HDBOKJI.GetValue().ToString()),
                                                            Get_Numeric(this.TXT01_HDTJCD.GetValue().ToString()),
                                                            Get_Numeric(this.TXT01_HDEDU.GetValue().ToString()),
                                                            Get_Numeric(this.TXT01_HDYBOKJI.GetValue().ToString()),
                                                            Get_Numeric(this.TXT01_HDYTJCD.GetValue().ToString()),
                                                            Get_Numeric(this.TXT01_HDYEDU.GetValue().ToString()),
                                                            Get_Numeric(this.TXT01_HDJJAKUPUP.GetValue().ToString()),
                                                            Get_Numeric(this.TXT01_HDBJAKUPUP.GetValue().ToString()),
                                                            Get_Numeric(this.TXT01_HDYJAKUPUP.GetValue().ToString()),
                                                            Get_Numeric(this.TXT01_HDBOKJIUP.GetValue().ToString()),
                                                            Get_Numeric(this.TXT01_HDTJCDUP.GetValue().ToString()),
                                                            Get_Numeric(this.TXT01_HDEDUUP.GetValue().ToString()),
                                                            Get_Numeric(this.TXT01_HDYBOKJIUP.GetValue().ToString()),
                                                            Get_Numeric(this.TXT01_HDYTJCDUP.GetValue().ToString()),
                                                            Get_Numeric(this.TXT01_HDYEDUUP.GetValue().ToString()),
                                                            Get_Numeric(this.TXT01_HDJJAKUPJN.GetValue().ToString()),
                                                            Get_Numeric(this.TXT01_HDBJAKUPJN.GetValue().ToString()),
                                                            Get_Numeric(this.TXT01_HDYJAKUPJN.GetValue().ToString()),
                                                            Get_Numeric(this.TXT01_HDBOKJIJN.GetValue().ToString()),
                                                            Get_Numeric(this.TXT01_HDTJCDJN.GetValue().ToString()),
                                                            Get_Numeric(this.TXT01_HDEDUJN.GetValue().ToString()),
                                                            Get_Numeric(this.TXT01_HDYBOKJIJN.GetValue().ToString()),
                                                            Get_Numeric(this.TXT01_HDYTJCDJN.GetValue().ToString()),
                                                            Get_Numeric(this.TXT01_HDYEDUJN.GetValue().ToString()),
                                                            Get_Numeric(this.TXT01_HDDANAMT.GetValue().ToString())
                                                            );
            }
            else
            {
                this.DbConnector.Attach("TY_P_US_96H8E856", Get_Numeric(this.TXT01_HDJJAKUP.GetValue().ToString()),
                                                            Get_Numeric(this.TXT01_HDBJAKUP.GetValue().ToString()),
                                                            Get_Numeric(this.TXT01_HDYJAKUP.GetValue().ToString()),
                                                            Get_Numeric(this.TXT01_HDBOKJI.GetValue().ToString()),
                                                            Get_Numeric(this.TXT01_HDTJCD.GetValue().ToString()),
                                                            Get_Numeric(this.TXT01_HDEDU.GetValue().ToString()),
                                                            Get_Numeric(this.TXT01_HDYBOKJI.GetValue().ToString()),
                                                            Get_Numeric(this.TXT01_HDYTJCD.GetValue().ToString()),
                                                            Get_Numeric(this.TXT01_HDYEDU.GetValue().ToString()),
                                                            Get_Numeric(this.TXT01_HDJJAKUPUP.GetValue().ToString()),
                                                            Get_Numeric(this.TXT01_HDBJAKUPUP.GetValue().ToString()),
                                                            Get_Numeric(this.TXT01_HDYJAKUPUP.GetValue().ToString()),
                                                            Get_Numeric(this.TXT01_HDBOKJIUP.GetValue().ToString()),
                                                            Get_Numeric(this.TXT01_HDTJCDUP.GetValue().ToString()),
                                                            Get_Numeric(this.TXT01_HDEDUUP.GetValue().ToString()),
                                                            Get_Numeric(this.TXT01_HDYBOKJIUP.GetValue().ToString()),
                                                            Get_Numeric(this.TXT01_HDYTJCDUP.GetValue().ToString()),
                                                            Get_Numeric(this.TXT01_HDYEDUUP.GetValue().ToString()),
                                                            Get_Numeric(this.TXT01_HDJJAKUPJN.GetValue().ToString()),
                                                            Get_Numeric(this.TXT01_HDBJAKUPJN.GetValue().ToString()),
                                                            Get_Numeric(this.TXT01_HDYJAKUPJN.GetValue().ToString()),
                                                            Get_Numeric(this.TXT01_HDBOKJIJN.GetValue().ToString()),
                                                            Get_Numeric(this.TXT01_HDTJCDJN.GetValue().ToString()),
                                                            Get_Numeric(this.TXT01_HDEDUJN.GetValue().ToString()),
                                                            Get_Numeric(this.TXT01_HDYBOKJIJN.GetValue().ToString()),
                                                            Get_Numeric(this.TXT01_HDYTJCDJN.GetValue().ToString()),
                                                            Get_Numeric(this.TXT01_HDYEDUJN.GetValue().ToString()),
                                                            Get_Numeric(this.TXT01_HDDANAMT.GetValue().ToString()),
                                                            Get_Date(this.DTP01_HDYYMM.GetValue().ToString())
                                                            );
            }
            this.DbConnector.ExecuteTranQuery();

            fsHDYYMM = DTP01_HDYYMM.GetValue().ToString();

            UP_DataBinding();

            this.ShowMessage("TY_M_GB_23NAD873");
        }
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            if (this.fsHDYYMM.ToString().Trim() != "")
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_US_96EFN843", Get_Date(this.DTP01_HDYYMM.GetValue().ToString()));

                DataTable dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.ShowMessage("TY_M_US_96EFP844");
                    e.Successed = false;
                    return;
                }
            }
            // 작업 차액
            if (Get_Numeric(this.TXT01_HDJJAKUPUP.GetValue().ToString()) != "0")
            {
                decimal dHDJJAKUPUP = Decimal.Parse(Get_Numeric(this.TXT01_HDJJAKUPUP.GetValue().ToString()));
                decimal dHDJJAKUP = Decimal.Parse(Get_Numeric(this.TXT01_HDJJAKUP.GetValue().ToString()));
                this.TXT01_HDJJAKUPJN.SetValue(Convert.ToString(dHDJJAKUPUP - dHDJJAKUP));
            }
            else
            {
                this.TXT01_HDJJAKUPJN.SetValue("0");
            }

            if (Get_Numeric(this.TXT01_HDBJAKUPUP.GetValue().ToString()) != "0")
            {
                decimal dHDBJAKUPUP = Decimal.Parse(Get_Numeric(this.TXT01_HDBJAKUPUP.GetValue().ToString()));
                decimal dHDBJAKUP = Decimal.Parse(Get_Numeric(this.TXT01_HDBJAKUP.GetValue().ToString()));
                this.TXT01_HDBJAKUPJN.SetValue(Convert.ToString(dHDBJAKUPUP - dHDBJAKUP));
            }
            else
            {
                this.TXT01_HDBJAKUPJN.SetValue("0");
            }

            if (Get_Numeric(this.TXT01_HDYJAKUPUP.GetValue().ToString()) != "0")
            {
                decimal dHDYJAKUPUP = Decimal.Parse(Get_Numeric(this.TXT01_HDYJAKUPUP.GetValue().ToString()));
                decimal dHDYJAKUP = Decimal.Parse(Get_Numeric(this.TXT01_HDYJAKUP.GetValue().ToString()));
                this.TXT01_HDYJAKUPJN.SetValue(Convert.ToString(dHDYJAKUPUP - dHDYJAKUP));
            }
            else
            {
                this.TXT01_HDYJAKUPJN.SetValue("0");
            }

            if (Get_Numeric(this.TXT01_HDBOKJIUP.GetValue().ToString()) != "0")
            {
                decimal dHDBOKJIUP = Decimal.Parse(Get_Numeric(this.TXT01_HDBOKJIUP.GetValue().ToString()));
                decimal dHDBOKJI = Decimal.Parse(Get_Numeric(this.TXT01_HDBOKJI.GetValue().ToString()));
                this.TXT01_HDBOKJIJN.SetValue(Convert.ToString(dHDBOKJIUP - dHDBOKJI));
            }
            else
            {
                this.TXT01_HDBOKJIJN.SetValue("0");
            }

            if (Get_Numeric(this.TXT01_HDTJCDUP.GetValue().ToString()) != "0")
            {
                decimal dHDTJCDUP = Decimal.Parse(Get_Numeric(this.TXT01_HDTJCDUP.GetValue().ToString()));
                decimal dHDTJCD = Decimal.Parse(Get_Numeric(this.TXT01_HDTJCD.GetValue().ToString()));
                this.TXT01_HDTJCDJN.SetValue(Convert.ToString(dHDTJCDUP - dHDTJCD));
            }
            else
            {
                this.TXT01_HDTJCDJN.SetValue("0");
            }

            if (Get_Numeric(this.TXT01_HDYTJCDUP.GetValue().ToString()) != "0")
            {
                decimal dHDYTJCDUP = Decimal.Parse(Get_Numeric(this.TXT01_HDYTJCDUP.GetValue().ToString()));
                decimal dHDYTJCD = Decimal.Parse(Get_Numeric(this.TXT01_HDYTJCD.GetValue().ToString()));
                this.TXT01_HDYTJCDJN.SetValue(Convert.ToString(dHDYTJCDUP - dHDYTJCD));
            }
            else
            {
                this.TXT01_HDYTJCDJN.SetValue("0");
            }

            if (Get_Numeric(this.TXT01_HDEDUUP.GetValue().ToString()) != "0")
            {
                decimal dHDEDUUP = Decimal.Parse(Get_Numeric(this.TXT01_HDEDUUP.GetValue().ToString()));
                decimal dHDEDU = Decimal.Parse(Get_Numeric(this.TXT01_HDEDU.GetValue().ToString()));
                this.TXT01_HDEDUJN.SetValue(Convert.ToString(dHDEDUUP - dHDEDU));
            }
            else
            {
                this.TXT01_HDEDUJN.SetValue("");
            }

            if (Get_Numeric(this.TXT01_HDYEDUUP.GetValue().ToString()) != "0")
            {
                decimal dHDYEDUUP = Decimal.Parse(Get_Numeric(this.TXT01_HDYEDUUP.GetValue().ToString()));
                decimal dHDYEDU = Decimal.Parse(Get_Numeric(this.TXT01_HDYEDU.GetValue().ToString()));
                this.TXT01_HDYEDUJN.SetValue(Convert.ToString(dHDYEDUUP - dHDYEDU));
            }
            else
            {
                this.TXT01_HDYEDUJN.SetValue("0");
            }

            if (!this.ShowMessage("TY_M_GB_23NAD871"))
            {
                e.Successed = false;
                return;
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
