using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.HR00
{
    /// <summary>
    /// 연장관리 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2014.11.25 20:09
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_4BPK2533 : 연장관리 등록
    ///  TY_P_HR_4BPK4535 : 연장관리 수정
    ///  TY_P_HR_4BPKA536 : 연장관리 확인
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
    ///  GYDGSABUN : 대근자
    ///  GYINSABUN : 입력자
    ///  GYSABUN : 사  번
    ///  GYGUBN : 신청형태
    ///  GYDATE : 연장일자
    ///  GYEDTIME : 종료시간
    ///  GYSAYU : 사 유
    ///  GYSTTIME : 시작시간
    /// </summary>
    public partial class TYHRCS001I : TYBase
    {
        private string fsFGYEAR;
        private string fsFGSEQ;
        private string fsFGYOILGN;

        #region  Description : 폼 로드 이벤트
        public TYHRCS001I(string sFGYEAR, string sFGSEQ, string sFGYOILGN)
        {
            InitializeComponent();

            this.fsFGYEAR   = sFGYEAR;
            this.fsFGSEQ    = sFGSEQ;
            this.fsFGYOILGN = sFGYOILGN;
        }

        private void TYHRCS001I_Load(object sender, System.EventArgs e)
        {
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);

            this.DTP01_FGJEGONGDATE1.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));
            this.DTP01_FGJEGONGDATE2.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));

            this.CBH01_FGSOSOK.DummyValue = DateTime.Now.ToString("yyyyMMdd");

            if (string.IsNullOrEmpty(this.fsFGYEAR) && string.IsNullOrEmpty(this.fsFGSEQ))
            {
                SetStartingFocus(this.TXT01_FGYEAR);
            }
            else
            {
                this.UP_FieldLock();
                this.UP_Run();
            }
        }
        #endregion

        #region  Description : 확인 UP_RUN 이벤트
        private void UP_Run()
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_614IM394", this.fsFGYEAR, this.fsFGSEQ, this.fsFGYOILGN);
            DataSet ds = this.DbConnector.ExecuteDataSet();

            if (ds.Tables[0].Rows.Count > 0)
            {
                this.CurrentDataTableRowMapping(ds.Tables[0], "01");
            }
        }
        #endregion

        #region Description : 필드 LOCK 이벤트
        private void UP_FieldLock()
        {
            this.TXT01_FGYEAR.SetReadOnly(true);
            this.TXT01_FGSEQ.SetReadOnly(true);
            this.CBO01_FGYOILGN.SetReadOnly(true);
        }
        #endregion

        #region  Description : 닫기 버튼 이벤트
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
        #endregion

        #region  Description : 저장 버튼 이벤트
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();
            if (string.IsNullOrEmpty(this.fsFGYEAR) && string.IsNullOrEmpty(this.fsFGSEQ))
            {
                this.DbConnector.Attach("TY_P_HR_614IN395", this.TXT01_FGYEAR.GetValue(),
                                                            this.TXT01_FGSEQ.GetValue(),
                                                            this.CBO01_FGYOILGN.GetValue(),
                                                            this.CBH01_FGVNCODE.GetValue(),
                                                            this.TXT01_FGVNNAME.GetValue(),
                                                            this.CBH01_FGSABUN.GetValue(),
                                                            this.CBH01_FGSOSOK.GetValue(),
                                                            this.TXT01_FGJOSINWON.GetValue(),
                                                            this.TXT01_FGJUSINWON.GetValue(),
                                                            this.TXT01_FGSESINWON.GetValue(),
                                                            this.TXT01_FGYASINWON.GetValue(),
                                                            this.CBO01_FGSIKBIGN.GetValue(),
                                                            this.DTP01_FGJEGONGDATE1.GetValue(),
                                                            this.DTP01_FGJEGONGDATE2.GetValue(),
                                                            this.TXT01_FGSAYU.GetValue(),
                                                            TYUserInfo.EmpNo
                                                            );
            }
            else
            {
                this.DbConnector.Attach("TY_P_HR_614IO396", this.CBH01_FGVNCODE.GetValue(),
                                                            this.TXT01_FGVNNAME.GetValue(),
                                                            this.CBH01_FGSABUN.GetValue(),
                                                            this.CBH01_FGSOSOK.GetValue(),
                                                            this.TXT01_FGJOSINWON.GetValue(),
                                                            this.TXT01_FGJUSINWON.GetValue(),
                                                            this.TXT01_FGSESINWON.GetValue(),
                                                            this.TXT01_FGYASINWON.GetValue(),
                                                            this.CBO01_FGSIKBIGN.GetValue(),
                                                            this.DTP01_FGJEGONGDATE1.GetValue(),
                                                            this.DTP01_FGJEGONGDATE2.GetValue(),
                                                            this.TXT01_FGSAYU.GetValue(),
                                                            TYUserInfo.EmpNo,
                                                            this.TXT01_FGYEAR.GetValue(),
                                                            this.TXT01_FGSEQ.GetValue(),
                                                            this.CBO01_FGYOILGN.GetValue()
                                                            );
            }

            this.DbConnector.ExecuteTranQuery();

            this.ShowMessage("TY_M_GB_23NAD873");
        }

        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            if (Convert.ToInt32(this.DTP01_FGJEGONGDATE1.GetValue().ToString()) > Convert.ToInt32(this.DTP01_FGJEGONGDATE2.GetValue().ToString()))
            {
                this.ShowCustomMessage("시작일자는 종료일자보다 이전이어야 합니다. ", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                e.Successed = false;
                return;
            }

            string sFGJOSINWON = string.Empty;
            string sFGJUSINWON = string.Empty;
            string sFGSESINWON = string.Empty;
            string sFGYASINWON = string.Empty;

            sFGJOSINWON = this.TXT01_FGJOSINWON.GetValue().ToString();
            sFGJUSINWON = this.TXT01_FGJUSINWON.GetValue().ToString();
            sFGSESINWON = this.TXT01_FGSESINWON.GetValue().ToString();
            sFGYASINWON = this.TXT01_FGYASINWON.GetValue().ToString();

            if ((int.Parse(Get_Numeric(sFGJOSINWON.ToString())) + int.Parse(Get_Numeric(sFGJUSINWON.ToString())) + int.Parse(Get_Numeric(sFGSESINWON.ToString())) + int.Parse(Get_Numeric(sFGYASINWON.ToString()))) == 0)
            {
                this.ShowCustomMessage("조식,중식,석식,야식 인원을 넣어 주세요. ", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                e.Successed = false;
                return;
            }

            if (this.CBH01_FGVNCODE.GetValue().ToString() == "" && this.TXT01_FGVNNAME.GetValue().ToString() == "")
            {
                this.ShowCustomMessage("거래처코드나 거래처명 둘중 하나는 반드시 등록하세요. ", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                e.Successed = false;
                SetFocus(this.CBH01_FGVNCODE);
                return;
            }

            if (string.IsNullOrEmpty(this.fsFGYEAR) && string.IsNullOrEmpty(this.fsFGSEQ))
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_6159N399", this.TXT01_FGYEAR.GetValue().ToString());
                DataSet ds = this.DbConnector.ExecuteDataSet();

                if (ds.Tables[0].Rows.Count > 0)
                {
                    this.TXT01_FGSEQ.SetValue(ds.Tables[0].Rows[0][0].ToString());
                }
            }

            if (!this.ShowMessage("TY_M_GB_23NAD871"))
            {
                e.Successed = false;
                return;
            }
        }
        #endregion
    }
}