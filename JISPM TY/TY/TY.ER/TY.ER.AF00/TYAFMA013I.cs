using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.AF00
{
    /// <summary>
    /// 자금항목 등록 팝업 프로그램입니다.
    /// 
    /// 작성자 : 김상권
    /// 작성일 : 2012.03.30 15:43
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_23U3Z210 : 자금항목코드 등록
    ///  TY_P_AC_23U40211 : 자금항목코드 수정
    ///  TY_P_AC_23U43214 : 자금항목코드 확인
    /// 
    ///  # 스프레드 정보 ####
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_GB_23NAD871 : 저장하시겠습니까?
    ///  TY_M_GB_23NAD873 : 저장하였습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  SAV : 저장
    ///  A3FDHL1 : 상위항목코드１
    ///  A3FDHL2 : 상위항목코드２
    ///  A3FDHL3 : 상위항목코드３
    ///  A3IDPL  : LEVEL
    ///  A3YNSL  : 전표발생단위Y/N
    ///  A3ABFD  : 자금항목약명
    ///  A3CDFD  : 자금항목코드
    ///  A3HISAB : 작성사번
    ///  A3NMFD  : 자금항목명
    /// </summary>
    public partial class TYAFMA013I : TYBase
    {
        private string fsESISUBGN;
        private string fsESIYYMM;

        #region Description : 페이지 로드
        public TYAFMA013I(string sESISUBGN, string sESIYYMM)
        {
            InitializeComponent();

            this.SetPopupStyle();

            // 파라미터값 가져오기
            this.fsESISUBGN = sESISUBGN;
            this.fsESIYYMM  = sESIYYMM;
        }

        private void TYAFMA013I_Load(object sender, System.EventArgs e)
        {
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);

            this.DTP01_ESIYYMM.SetReadOnly(true);

            this.CBH01_ESISUBGN.SetValue(fsESISUBGN.ToString());
            this.DTP01_ESIYYMM.SetValue(fsESIYYMM.ToString());


            if (string.IsNullOrEmpty(this.fsESISUBGN)) // 등록
            {
                this.CBH01_ESISUBGN.SetReadOnly(false);
                this.DTP01_ESIYYMM.SetReadOnly(false);

                SetStartingFocus(this.CBH01_ESISUBGN.CodeText);
            }
            else // 수정
            {
                this.CBH01_ESISUBGN.SetReadOnly(true);
                this.DTP01_ESIYYMM.SetReadOnly(true);

                SetStartingFocus(this.TXT01_ESISTITLE1);

                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_AC_3BE2L298",
                    this.CBH01_ESISUBGN.GetValue().ToString(),
                    this.DTP01_ESIYYMM.GetValue().ToString()
                    );

                DataTable dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                    this.CurrentDataTableRowMapping(dt, "01");
            }
        }
        #endregion

        #region Description : 저장 버튼
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            string sESISTITLE = string.Empty;
            string sESIISSUE  = string.Empty;
            string sESICORRE  = string.Empty;

            // 삭제
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_3BE1L289", this.CBH01_ESISUBGN.GetValue().ToString(),
                                                        this.DTP01_ESIYYMM.GetValue().ToString());
            this.DbConnector.ExecuteNonQuery();

            // 등록
            for (int i = 1; i <= 5; i++)
            {
                switch(i)
                {
                    case 1:
                        sESISTITLE = this.TXT01_ESISTITLE1.GetValue().ToString();
                        sESIISSUE  = this.TXT01_ESIISSUE1.GetValue().ToString();
                        sESICORRE  = this.TXT01_ESICORRE1.GetValue().ToString();
                        break;
                    case 2:
                        sESISTITLE = this.TXT01_ESISTITLE2.GetValue().ToString();
                        sESIISSUE  = this.TXT01_ESIISSUE2.GetValue().ToString();
                        sESICORRE  = this.TXT01_ESICORRE2.GetValue().ToString();
                        break;
                    case 3:
                        sESISTITLE = this.TXT01_ESISTITLE3.GetValue().ToString();
                        sESIISSUE  = this.TXT01_ESIISSUE3.GetValue().ToString();
                        sESICORRE  = this.TXT01_ESICORRE3.GetValue().ToString();
                        break;
                    case 4:
                        sESISTITLE = this.TXT01_ESISTITLE4.GetValue().ToString();
                        sESIISSUE  = this.TXT01_ESIISSUE4.GetValue().ToString();
                        sESICORRE  = this.TXT01_ESICORRE4.GetValue().ToString();
                        break;
                    case 5:
                        sESISTITLE = this.TXT01_ESISTITLE5.GetValue().ToString();
                        sESIISSUE  = this.TXT01_ESIISSUE5.GetValue().ToString();
                        sESICORRE  = this.TXT01_ESICORRE5.GetValue().ToString();
                        break;
                }

                if (sESISTITLE.ToString() != "")
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_AC_3BE1L290", this.CBH01_ESISUBGN.GetValue().ToString(),
                                                                this.DTP01_ESIYYMM.GetValue().ToString(),
                                                                Convert.ToString(i),
                                                                sESISTITLE.ToString(),
                                                                sESIISSUE.ToString(),
                                                                sESICORRE.ToString(),
                                                                TYUserInfo.EmpNo
                                                                );
                    this.DbConnector.ExecuteNonQuery();
                }
            }

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.ShowMessage("TY_M_GB_23NAD873");
            this.Close();
        }
        #endregion

        #region Description : 저장 체크
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            if (this.TXT01_ESISTITLE1.GetValue().ToString() == "" &&
                this.TXT01_ESIISSUE1.GetValue().ToString()  == "" &&
                this.TXT01_ESICORRE1.GetValue().ToString()  == "" &&
                this.TXT01_ESISTITLE2.GetValue().ToString() == "" &&
                this.TXT01_ESIISSUE2.GetValue().ToString()  == "" &&
                this.TXT01_ESICORRE2.GetValue().ToString()  == "" &&
                this.TXT01_ESISTITLE3.GetValue().ToString() == "" &&
                this.TXT01_ESIISSUE3.GetValue().ToString()  == "" &&
                this.TXT01_ESICORRE3.GetValue().ToString()  == "" &&
                this.TXT01_ESISTITLE4.GetValue().ToString() == "" &&
                this.TXT01_ESIISSUE4.GetValue().ToString()  == "" &&
                this.TXT01_ESICORRE4.GetValue().ToString()  == "" &&
                this.TXT01_ESISTITLE5.GetValue().ToString() == "" &&
                this.TXT01_ESIISSUE5.GetValue().ToString()  == "" &&
                this.TXT01_ESICORRE5.GetValue().ToString()  == ""
                )
            {
                this.ShowMessage("TY_M_AC_3BE1Q291");
                e.Successed = false;
                return;
            }

            if (this.TXT01_ESISTITLE1.GetValue().ToString() == "" && (this.TXT01_ESIISSUE1.GetValue().ToString() != "" ||
                                                                      this.TXT01_ESICORRE1.GetValue().ToString() != ""))
            {
                this.SetFocus(this.TXT01_ESISTITLE1);

                this.ShowMessage("TY_M_AC_3BE1Q291");
                e.Successed = false;
                return;
            }

            if (this.TXT01_ESISTITLE2.GetValue().ToString() == "" && (this.TXT01_ESIISSUE2.GetValue().ToString() != "" ||
                                                                      this.TXT01_ESICORRE2.GetValue().ToString() != ""))
            {
                this.SetFocus(this.TXT01_ESISTITLE2);

                this.ShowMessage("TY_M_AC_3BE1Q291");
                e.Successed = false;
                return;
            }

            if (this.TXT01_ESISTITLE3.GetValue().ToString() == "" && (this.TXT01_ESIISSUE3.GetValue().ToString() != "" ||
                                                                      this.TXT01_ESICORRE3.GetValue().ToString() != ""))
            {
                this.SetFocus(this.TXT01_ESISTITLE3);

                this.ShowMessage("TY_M_AC_3BE1Q291");
                e.Successed = false;
                return;
            }

            if (this.TXT01_ESISTITLE4.GetValue().ToString() == "" && (this.TXT01_ESIISSUE4.GetValue().ToString() != "" ||
                                                                      this.TXT01_ESICORRE4.GetValue().ToString() != ""))
            {
                this.SetFocus(this.TXT01_ESISTITLE4);

                this.ShowMessage("TY_M_AC_3BE1Q291");
                e.Successed = false;
                return;
            }

            if (this.TXT01_ESISTITLE5.GetValue().ToString() == "" && (this.TXT01_ESIISSUE5.GetValue().ToString() != "" ||
                                                                      this.TXT01_ESICORRE5.GetValue().ToString() != ""))
            {
                this.SetFocus(this.TXT01_ESISTITLE5);

                this.ShowMessage("TY_M_AC_3BE1Q291");
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

        #region Description : 포커스 이동
        private void TXT01_ESISTITLE1_KeyDown(object sender, KeyEventArgs e)
        {
            if (Convert.ToInt32(e.KeyCode) == 17)
            {
                this.SetFocus(this.TXT01_ESIISSUE1);
            }
        }

        private void TXT01_ESIISSUE1_KeyDown(object sender, KeyEventArgs e)
        {
            if (Convert.ToInt32(e.KeyCode) == 17)
            {
                this.SetFocus(this.TXT01_ESICORRE1);
            }
        }

        private void TXT01_ESICORRE1_KeyDown(object sender, KeyEventArgs e)
        {
            if (Convert.ToInt32(e.KeyCode) == 17)
            {
                this.SetFocus(this.TXT01_ESISTITLE2);
            }
        }

        private void TXT01_ESISTITLE2_KeyDown(object sender, KeyEventArgs e)
        {
            if (Convert.ToInt32(e.KeyCode) == 17)
            {
                this.SetFocus(this.TXT01_ESIISSUE2);
            }
        }

        private void TXT01_ESIISSUE2_KeyDown(object sender, KeyEventArgs e)
        {
            if (Convert.ToInt32(e.KeyCode) == 17)
            {
                this.SetFocus(this.TXT01_ESICORRE2);
            }
        }

        private void TXT01_ESICORRE2_KeyDown(object sender, KeyEventArgs e)
        {
            if (Convert.ToInt32(e.KeyCode) == 17)
            {
                this.SetFocus(this.TXT01_ESISTITLE3);
            }
        }

        private void TXT01_ESISTITLE3_KeyDown(object sender, KeyEventArgs e)
        {
            if (Convert.ToInt32(e.KeyCode) == 17)
            {
                this.SetFocus(this.TXT01_ESIISSUE3);
            }
        }

        private void TXT01_ESIISSUE3_KeyDown(object sender, KeyEventArgs e)
        {
            if (Convert.ToInt32(e.KeyCode) == 17)
            {
                this.SetFocus(this.TXT01_ESICORRE3);
            }
        }

        private void TXT01_ESICORRE3_KeyDown(object sender, KeyEventArgs e)
        {
            if (Convert.ToInt32(e.KeyCode) == 17)
            {
                this.SetFocus(this.TXT01_ESISTITLE4);
            }
        }

        private void TXT01_ESISTITLE4_KeyDown(object sender, KeyEventArgs e)
        {
            if (Convert.ToInt32(e.KeyCode) == 17)
            {
                this.SetFocus(this.TXT01_ESIISSUE4);
            }
        }

        private void TXT01_ESIISSUE4_KeyDown(object sender, KeyEventArgs e)
        {
            if (Convert.ToInt32(e.KeyCode) == 17)
            {
                this.SetFocus(this.TXT01_ESICORRE4);
            }
        }

        private void TXT01_ESICORRE4_KeyDown(object sender, KeyEventArgs e)
        {
            if (Convert.ToInt32(e.KeyCode) == 17)
            {
                this.SetFocus(this.TXT01_ESISTITLE5);
            }
        }

        private void TXT01_ESISTITLE5_KeyDown(object sender, KeyEventArgs e)
        {
            if (Convert.ToInt32(e.KeyCode) == 17)
            {
                this.SetFocus(this.TXT01_ESIISSUE5);
            }
        }

        private void TXT01_ESIISSUE5_KeyDown(object sender, KeyEventArgs e)
        {
            if (Convert.ToInt32(e.KeyCode) == 17)
            {
                this.SetFocus(this.TXT01_ESICORRE5);
            }
        }

        private void TXT01_ESICORRE5_KeyDown(object sender, KeyEventArgs e)
        {
            if (Convert.ToInt32(e.KeyCode) == 17)
            {
                this.SetFocus(this.BTN61_SAV);
            }
        }
        #endregion
    }
}