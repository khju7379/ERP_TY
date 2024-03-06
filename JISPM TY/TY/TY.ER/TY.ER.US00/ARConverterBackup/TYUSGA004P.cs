using System;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls; 
using TY.Service.Library;
using TY.ER.GB00;
using Shoveling2010.SmartClient.SystemUtility.Controls.FpSpreadCellType;
using DataDynamics.ActiveReports;
using ThoughtWorks.QRCode.Codec;

namespace TY.ER.US00
{
    /// <summary>
    /// 수동 출고증 출력 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2019.08.14 16:46
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_US_98EBB115 : 수동 출고증 출력 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_US_98EBJ116 : 수동 출고증 출력 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  PRT : 출력
    ///  CHPRTGB : 내부출력
    ///  CHCHULDAT : 출고일자
    ///  CHDCHULNUM : D연결번호
    ///  CHNUMBER : 차량번호
    ///  CHTKNO : TICKET번호
    /// </summary>
    public partial class TYUSGA004P : TYBase
    {
        string fsGUBUN = string.Empty;

        #region Description : 폼 로드
        public TYUSGA004P()
        {
            InitializeComponent();
        }

        public TYUSGA004P(string sGUBUN)
        {
            InitializeComponent();

            fsGUBUN = sGUBUN;
        }

        private void TYUSGA004P_Load(object sender, System.EventArgs e)
        {
            (this.FPS91_TY_S_US_98EBJ116.Sheets[0].Columns[24].Editor as TButtonCellType).Picture = global::TY.Service.Library.Properties.Resources.printer;
            this.SetSpreadFixedWidthColumn(this.FPS91_TY_S_US_98EBJ116, "BTNPRT");

            this.DTP01_CHCHULDAT.SetValue(System.DateTime.Now.ToString("yyyy-MM-dd"));

            SetStartingFocus(this.DTP01_CHCHULDAT);

            BTN61_INQ_Click(null, null);

            if (fsGUBUN != "")
            {
                this.BTN61_CLO.Visible = true;
            }
            else
            {
                this.BTN61_CLO.Visible = false;

                this.BTN61_PRT.Location = new System.Drawing.Point(1095, 12);
                this.BTN61_INQ.Location = new System.Drawing.Point(1014, 12);
            }
            this.LBL_MESSAGE.Text = "";
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_US_98EBB115",
                this.DTP01_CHCHULDAT.GetString(),
                this.TXT01_CHTKNO.GetValue().ToString(),
                this.TXT01_CHNUMBER.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_US_98EBJ116.SetValue(dt);
        }
        #endregion

        #region Description : 출력 버튼
        private void BTN61_PRT_Click(object sender, EventArgs e)
        {
            UP_SiloPrint(this.DTP01_CHCHULDAT.GetString(),
                         this.TXT01_CHTKNO.GetValue().ToString(),
                         "");
        }
        #endregion

        #region Description : 스프레드 출력 버튼 클릭
        private void FPS91_TY_S_US_98EBJ116_ButtonClicked(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            UP_SiloPrint(this.FPS91_TY_S_US_98EBJ116.GetValue("CHCHULDAT").ToString(),
                         this.FPS91_TY_S_US_98EBJ116.GetValue("CHTKNO").ToString(),
                         this.FPS91_TY_S_US_98EBJ116.GetValue("CHDCHULNUM").ToString());
        }
        #endregion

        #region Description : 출고증 출력
        private void UP_SiloPrint(string sCHCHULDAT, string sCHTKNO, string sCHDCHULNUM)
        {
            try
            {
                string sTKNO = string.Empty;
                double dCHEMPTY = 0;
                double dCHMTQTY = 0;
                double dCHTOTAL = 0;

                DataTable dt = new DataTable();

                if (Get_Numeric(sCHDCHULNUM) != "0")
                {
                    // 잔량처리

                    // 잔량 처리 데이터 조회
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_US_98EGB120",
                                            sCHCHULDAT,
                                            sCHDCHULNUM);

                    dt = this.DbConnector.ExecuteDataTable();

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (sTKNO != "")
                        {
                            sTKNO += "," + dt.Rows[i]["CHTKNO"].ToString();
                        }
                        else
                        {
                            sTKNO = dt.Rows[i]["CHTKNO"].ToString();
                            dCHEMPTY = Convert.ToDouble(dt.Rows[i]["CHEMPTY"].ToString());
                        }
                        dCHMTQTY += Convert.ToDouble(dt.Rows[i]["CHMTQTY"].ToString());
                    }

                    dCHTOTAL = dCHEMPTY + dCHMTQTY;
                }

                // 출고 데이터 조회
                this.DbConnector.CommandClear();
                //this.DbConnector.Attach("TY_P_US_98EGY121",
                this.DbConnector.Attach("TY_P_US_B1E9N308",
                                        sTKNO,
                                        sTKNO,
                                        dCHTOTAL,
                                        dCHTOTAL,
                                        dCHMTQTY,
                                        dCHMTQTY,
                                        sCHCHULDAT,
                                        sCHTKNO);

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    System.Drawing.Image qrimage = null;

                    if (dt.Rows[0]["CHHWAJUCODE"].ToString() == "J29")
                    {
                        qrimage = UP_SetQrcode(dt.Rows[0]["ID"].ToString(),           // 차량번호
                                               dt.Rows[0]["TRUNNAME"].ToString(),     // 운전자명
                                               dt.Rows[0]["CHBONSUN"].ToString(),     // 본선명
                                               dt.Rows[0]["CHHWAJUCODE"].ToString(),  // 화주코드
                                               dt.Rows[0]["TARE"].ToString(),         // 공차중량
                                               dt.Rows[0]["GROSS"].ToString(),        // 실차중량
                                               dt.Rows[0]["CHCHULDAT"].ToString(),    // 출고일자
                                               dt.Rows[0]["CHGOKJONGCODE"].ToString(),// 곡종코드
                                               dt.Rows[0]["CHGOKJONGNM"].ToString(),  // 곡종명
                                               dt.Rows[0]["NET"].ToString()           // 실중량
                                                   );
                    }

                    // 특정 프린터 출력
                    ActiveReport rpt = new TYUSGA004R(qrimage);

                    rpt.PageSettings.Orientation = DataDynamics.ActiveReports.Document.PageOrientation.Landscape;

                    rpt.DataSource = dt;
                    rpt.Run(false);

                    this.AVW01_REPORT.Document = rpt.Document;

                    if (this.CKB01_CHPRTGB.Checked == false)
                    {
                        // 외부출력
                        this.AVW01_REPORT.Document.Printer.PrinterName = "SILOGAGUN";
                    }

                    this.AVW01_REPORT.Document.Print(false, false, false);
                }
                this.LBL_MESSAGE.Text = "전표가 발행 되었습니다.";

                // FOCUS
                Timer tmr = new Timer();

                tmr.Tick += delegate
                {
                    tmr.Stop();
                    this.LBL_MESSAGE.Text = "";
                };

                tmr.Interval = 1500;
                tmr.Start();

            }
            catch
            {
                this.ShowCustomMessage("출고증 출력 중 오류가 발생하였습니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return;
            }
        }
        #endregion

        #region Description : QRCODE 생성 OK
        private System.Drawing.Image UP_SetQrcode(string sCARNO, string sDRIVERNM, string sBONSUN, string sHWAJU, string sTARE, string sGROSS,
                                                  string sCHULDAT, string sGOKJONG, string sGOKJONGNM, string sNET)
        {
            // 차량번호, 운전자명, 본선명, 화주코드, 공차중량, 실차중량, 곡종, 곡종명, 총중량 
            // 생성할 이미지 사이즈 정함.
            int sizeNum = 1;
            int level = 0;

            string sCHEMPY = string.Empty;
            string sCHTOTAL = string.Empty;
            string sCHMTQTY = string.Empty;
            string sCHGOKJONG = string.Empty;

            if (sGOKJONG == "12")
            {
                sCHGOKJONG = "31-0010";
            }
            else if (sGOKJONG == "15")
            {
                sCHGOKJONG = "31-0020";
            }
            else
            {
                sCHGOKJONG = sGOKJONG;
            }

            sCHEMPY = string.Format("{0:#}", (Convert.ToDouble(Get_Numeric(sTARE)) * 1000));
            sCHTOTAL = string.Format("{0:#}", (Convert.ToDouble(Get_Numeric(sGROSS)) * 1000));
            sCHMTQTY = string.Format("{0:#}", (Convert.ToDouble(Get_Numeric(sNET)) * 1000));

            StringBuilder cardeFormat = new StringBuilder();

            // 차량 운행 종류(고정:0) , 차량 번호, 차량 종류(고정:2), 운전자 이름, 업체 코드(고정:21012), 업체 이름(고정:(주)태영인더스트리), 모선명, 
            // 차량 공차 중량, 차량 총 중량, 발행일자(년월일시분초), 
            // 창고코드(고정:105), 원료코드(고정/옥수수:31-0010 소맥:31-0020), 원료 이름, 원료 유형(고정:RM), 단위 코드(고정:KG), 무게(실중량), 수량(고정:1), 포장 코드(고정:BLK)
            cardeFormat.Append("0~" + sCARNO + "~2~" + sDRIVERNM + "~21012~(주)태영인더스트리~" + sBONSUN + "~" + sCHEMPY + "~" + sCHTOTAL + "~" + sCHULDAT.Replace("-","").Replace(".","") +
                               "~105^" + sCHGOKJONG + "^" + sGOKJONGNM + "(수입)^RM^KG^" + sCHMTQTY + "^1^BLK^");

            // QRCodeEncoder 인스턴스 생성
            QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
            // 인코딩모드는 바이트로 설정.
            qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
            // QRCode 사이즈 지정.
            qrCodeEncoder.QRCodeScale = sizeNum;

            qrCodeEncoder.QRCodeVersion = level;
            // 에러 보정 레벨을 지정.
            qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;

            System.Drawing.Image rtnImage = null;

            string data = cardeFormat.ToString();
            try
            {
                // QRCode 이미지를 생성해 줌.
                rtnImage = qrCodeEncoder.Encode(data);
            }
            catch (Exception ex)
            {

            }

            return rtnImage;
        }
        #endregion

        #region Description : 닫기 버튼
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion
    }
}
