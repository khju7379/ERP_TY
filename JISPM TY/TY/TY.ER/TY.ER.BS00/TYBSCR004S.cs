using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.BS00
{
    /// <summary>
    /// 영업계획(매출액,취급량) 조회 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2017.08.08 10:04
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_788A4373 : 사업계획-영업계획(매출액,취급량) 자료 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_788A2372 : 영업계획(매출액,취급량) 자료 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  INQOPTION : 조회구분
    ///  INQOPTION2 : 조회구분
    ///  BSYEAR : 년도
    /// </summary>
    public partial class TYBSCR004S : TYBase
    {
        #region  Description : 폼 로드 이벤트
        public TYBSCR004S()
        {
            InitializeComponent();
        }

        private void TYBSCR004S_Load(object sender, System.EventArgs e)
        {
            this.TXT01_BCYEAR.SetValue(DateTime.Now.AddYears(1).ToString("yyyy"));
            
            CBH01_BCDPAC.DummyValue = this.TXT01_BCYEAR.GetValue().ToString() + "0101";

            this.SetStartingFocus(this.TXT01_BCYEAR);
        }
        #endregion

        #region  Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            
            this.FPS91_TY_S_AC_78ADF415.Initialize();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_78AE7417", TXT01_BCYEAR.GetValue(), CBH01_BCDPAC.GetValue(), CBO01_INQ_GROUPDPAC.GetValue(), CBO01_INQOPTION.GetValue(), CBO01_INQ_DIV.GetValue());
            this.FPS91_TY_S_AC_78ADF415.SetValue(UP_Set_SumRowAdd(this.DbConnector.ExecuteDataTable()));

            if (this.FPS91_TY_S_AC_78ADF415.CurrentRowCount > 0)
            {
                for (int i = 0; i < this.FPS91_TY_S_AC_78ADF415.CurrentRowCount; i++)
                {
                    if (this.FPS91_TY_S_AC_78ADF415.GetValue(i, "BCLVAC").ToString() == "5")
                    {
                        this.FPS91_TY_S_AC_78ADF415.ActiveSheet.Rows[i].BackColor = Color.FromArgb(228, 242, 194);

                        if (CBO01_INQOPTION.GetValue().ToString() == "2" || CBO01_INQOPTION.GetValue().ToString() == "3")
                        {
                            for (int j = 9; j < 22; j++)
                            {
                                this.FPS91_TY_S_AC_78ADF415_Sheet1.Cells[i, j].ForeColor = Color.Red;
                            }
                        }
                    }

                    if (this.FPS91_TY_S_AC_78ADF415.GetValue(i, "BCADACNM").ToString() == "[ 합 계 ]")
                    {
                        this.FPS91_TY_S_AC_78ADF415.ActiveSheet.Rows[i].BackColor = Color.FromArgb(218, 239, 244);
                    }
                }

                
            }
        }
        #endregion       

        #region Description : 소계,합계 Row 추가 함수
        private DataTable UP_Set_SumRowAdd(DataTable dt)
        {
            int i = 0;

            string sFilter = string.Empty;         

            DataTable table = new DataTable();

            table = dt;

            DataRow row;

            int nNum = table.Rows.Count;
           
            if (nNum > 0)
            {
                /******* 마지막 거래처의 대한 로우 생성*****/
                row = table.NewRow();
                table.Rows.InsertAt(row, nNum);

                table.Rows[nNum]["BCADACNM"] = "[ 합 계 ]";

                // 상위계정 레빌
                sFilter = "  BCLVAC  = '5' ";                

                table.Rows[nNum]["BCMONAMT01"] = table.Compute("SUM(BCMONAMT01)", sFilter).ToString();
                table.Rows[nNum]["BCMONAMT02"] = table.Compute("SUM(BCMONAMT02)", sFilter).ToString();
                table.Rows[nNum]["BCMONAMT03"] = table.Compute("SUM(BCMONAMT03)", sFilter).ToString();
                table.Rows[nNum]["BCMONAMT04"] = table.Compute("SUM(BCMONAMT04)", sFilter).ToString();
                table.Rows[nNum]["BCMONAMT05"] = table.Compute("SUM(BCMONAMT05)", sFilter).ToString();
                table.Rows[nNum]["BCMONAMT06"] = table.Compute("SUM(BCMONAMT06)", sFilter).ToString();
                table.Rows[nNum]["BCMONAMT07"] = table.Compute("SUM(BCMONAMT07)", sFilter).ToString();
                table.Rows[nNum]["BCMONAMT08"] = table.Compute("SUM(BCMONAMT08)", sFilter).ToString();
                table.Rows[nNum]["BCMONAMT09"] = table.Compute("SUM(BCMONAMT09)", sFilter).ToString();
                table.Rows[nNum]["BCMONAMT10"] = table.Compute("SUM(BCMONAMT10)", sFilter).ToString();
                table.Rows[nNum]["BCMONAMT11"] = table.Compute("SUM(BCMONAMT11)", sFilter).ToString();
                table.Rows[nNum]["BCMONAMT12"] = table.Compute("SUM(BCMONAMT12)", sFilter).ToString();
                table.Rows[nNum]["BCMONTOTAL"] = table.Compute("SUM(BCMONTOTAL)", sFilter).ToString();



                ///******** 총계를 위한 Row 생성 **************/
                //row = table.NewRow();
                //table.Rows.InsertAt(row, i + 1);

                //// 합 계 이름 넣기
                //table.Rows[i + 1]["BVDPACNM"] = "[ 합 계 ]";

                //table.Rows[i + 1]["BVMONAMT01"] = string.Format("{0:#,##0}", dBVMONAMT01);
                //table.Rows[i + 1]["BVMONAMT02"] = string.Format("{0:#,##0}", dBVMONAMT02);
                //table.Rows[i + 1]["BVMONAMT03"] = string.Format("{0:#,##0}", dBVMONAMT03);
                //table.Rows[i + 1]["BVMONAMT04"] = string.Format("{0:#,##0}", dBVMONAMT04);
                //table.Rows[i + 1]["BVMONAMT05"] = string.Format("{0:#,##0}", dBVMONAMT05);
                //table.Rows[i + 1]["BVMONAMT06"] = string.Format("{0:#,##0}", dBVMONAMT06);
                //table.Rows[i + 1]["BVMONAMT07"] = string.Format("{0:#,##0}", dBVMONAMT07);
                //table.Rows[i + 1]["BVMONAMT08"] = string.Format("{0:#,##0}", dBVMONAMT08);
                //table.Rows[i + 1]["BVMONAMT09"] = string.Format("{0:#,##0}", dBVMONAMT09);
                //table.Rows[i + 1]["BVMONAMT10"] = string.Format("{0:#,##0}", dBVMONAMT10);
                //table.Rows[i + 1]["BVMONAMT11"] = string.Format("{0:#,##0}", dBVMONAMT11);
                //table.Rows[i + 1]["BVMONAMT12"] = string.Format("{0:#,##0}", dBVMONAMT12);
                //table.Rows[i + 1]["BVMONTOTAL"] = string.Format("{0:#,##0}", dBVMONTOTAL);

            }

            return table;

            
        }
        #endregion          
        
        #region Description : CBH01_BCDPAC_CodeBoxDataBinded 함수
        private void CBH01_BCDPAC_CodeBoxDataBinded(object sender, EventArgs e)
        {
            if (CBH01_BCDPAC.GetValue().ToString() != "")
            {
                CBO01_INQ_GROUPDPAC.SetReadOnly(true);
            }
            else
            {
                CBO01_INQ_GROUPDPAC.SetReadOnly(false);
            }

            if (CBH01_BCDPAC.GetText().ToString() != "")
            {
                CBO01_INQ_GROUPDPAC.SetReadOnly(true);
            }
            else
            {
                CBO01_INQ_GROUPDPAC.SetReadOnly(false);
            }


        }

        private void CBH01_BCDPAC_KeyUp(object sender, KeyEventArgs e)
        {
            if (CBH01_BCDPAC.GetValue().ToString() != "")
            {
                CBO01_INQ_GROUPDPAC.SetReadOnly(true);
            }
            else
            {
                CBO01_INQ_GROUPDPAC.SetReadOnly(false);
            }

            if (CBH01_BCDPAC.GetText().ToString() != "")
            {
                CBO01_INQ_GROUPDPAC.SetReadOnly(true);
            }
            else
            {
                CBO01_INQ_GROUPDPAC.SetReadOnly(false);
            }
        }
        #endregion

       

      
    }
}
