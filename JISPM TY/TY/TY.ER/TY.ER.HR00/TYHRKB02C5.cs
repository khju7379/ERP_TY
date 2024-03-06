using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.HR00
{
    /// <summary>
    /// 가족사항 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2014.11.12 15:23
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_4BD9O400 : 가족사항 등록
    ///  TY_P_HR_4BD9P401 : 가족사항 수정
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
    ///  GJCODE : 가족코드
    ///  HLSABUN : 사번
    ///  GJSEXGB : 성별
    ///  GJJUMIN : 주민번호6자리
    ///  GJNAME : 가족이름
    ///  HLCOMPANY : 회사
    ///  SEQ : 순서
    /// </summary>
    public partial class TYHRKB02C5 : TYBase
    {
        string fsGJSABUN = string.Empty;
        string fsGJSEQ = string.Empty;

        #region Description : 페이지 로드
        public TYHRKB02C5(string GJSABUN, string GJSEQ)
        {
            fsGJSABUN = GJSABUN;
            fsGJSEQ = GJSEQ;

            InitializeComponent();
        }

        private void TYHRKB02C5_Load(object sender, System.EventArgs e)
        {
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);

            if (string.IsNullOrEmpty(fsGJSEQ))
            {
                CBH01_HLSABUN.SetValue(fsGJSABUN);
                UP_GetSEQ();
            }
            else
            {   
                UP_Select();
            }
            SetStartingFocus(CBH01_GJCODE.CodeText);
            CBH01_HLSABUN.SetReadOnly(true);
            TXT01_SEQ.SetReadOnly(true);
        }
        #endregion

        #region Description : 저장 버튼
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();

            if (string.IsNullOrEmpty(fsGJSEQ))  //등록
            {
                UP_GetSEQ();

                this.DbConnector.Attach("TY_P_HR_4BD9O400", CBH01_HLSABUN.GetValue().ToString(),
                                                            TXT01_SEQ.GetValue().ToString(),
                                                            CBH01_GJCODE.GetValue().ToString(),
                                                            TXT01_GJNAME.GetValue().ToString(),
                                                            CBO01_GJSEXGB.GetValue().ToString(),
                                                            MTB01_GJJUMIN.GetValue().ToString().Replace("-", "").Trim(),
                                                            TYUserInfo.SecureKey,
                                                            TXT01_GJHAKLK.GetValue().ToString(),
                                                            TXT01_GJJIKUP.GetValue().ToString(),
                                                            TYUserInfo.EmpNo
                                                            ); 
            }
            else                                //수정
            {
                this.DbConnector.Attach("TY_P_HR_4BD9P401", CBH01_GJCODE.GetValue().ToString(),
                                                            TXT01_GJNAME.GetValue().ToString(),
                                                            CBO01_GJSEXGB.GetValue().ToString(),
                                                            MTB01_GJJUMIN.GetValue().ToString().Replace("-", "").Trim(),
                                                            TYUserInfo.SecureKey,
                                                            TXT01_GJHAKLK.GetValue().ToString(),
                                                            TXT01_GJJIKUP.GetValue().ToString(),
                                                            TYUserInfo.EmpNo,
                                                            CBH01_HLSABUN.GetValue().ToString(),
                                                            TXT01_SEQ.GetValue().ToString()
                                                            ); 
            }
            this.DbConnector.ExecuteNonQuery();

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.ShowMessage("TY_M_GB_23NAD873");
            this.Close();
        }
        #endregion

        #region Description : 저장 체크
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            //개인정보취급자가 아니면
            if (TYUserInfo.PerAuth != "Y")
            {
                this.ShowCustomMessage("개인정보취급자이외는 저장 할수 없습니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                e.Successed = false;
                return;
            }

            if (!this.ShowMessage("TY_M_GB_23NAD871"))
            {
                e.Successed = false;
                return;
            }
        }
        #endregion

        #region Description : 닫기 버튼
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Description : 데이터 조회
        private void UP_Select()
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_4BDFB420", TYUserInfo.SecureKey, TYUserInfo.PerAuth,
                                                        fsGJSABUN,
                                                        fsGJSEQ
                                                        );
            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                CBH01_HLSABUN.SetValue(fsGJSABUN);
                TXT01_SEQ.SetValue(Set_Fill3(Convert.ToInt16(fsGJSEQ).ToString()));
                CBH01_GJCODE.SetValue(dt.Rows[0]["GJCODE"].ToString());
                TXT01_GJNAME.SetValue(dt.Rows[0]["GJNAME"].ToString());
                MTB01_GJJUMIN.SetValue(dt.Rows[0]["GJJUMIN"].ToString());
                CBO01_GJSEXGB.SetValue(dt.Rows[0]["GJSEXGB"].ToString());
                TXT01_GJHAKLK.SetValue(dt.Rows[0]["GJHAKLK"].ToString());
                TXT01_GJJIKUP.SetValue(dt.Rows[0]["GJJIKUP"].ToString());
            }
        }
        #endregion

        #region Description : 순번 가져오기
        private void UP_GetSEQ()
        {
            string SEQ = string.Empty;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_4BDFD421", fsGJSABUN
                                                        );

            Int16 iCnt = Convert.ToInt16(this.DbConnector.ExecuteScalar());

            this.TXT01_SEQ.SetValue(Set_Fill3(iCnt.ToString()));
        }
        #endregion



    }
}
