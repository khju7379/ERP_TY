using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library; 
using TY.Service.Library.Controls;

using System.IO;
using System.Windows.Forms;

using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Printing;

namespace TY.ER.MR00
{
    /// <summary>
    /// 품목 코드 관리(팝업) 프로그램입니다.
    /// 
    /// 작성자 : 김상권
    /// 작성일 : 2012.11.06 13:01
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_MR_2B62Y135 : 품목코드 등록
    ///  TY_P_MR_2B62Z136 : 품목코드 수정
    ///  TY_P_MR_2B631138 : 품목코드 확인
    ///  TY_P_MR_2B68N147 : 품목코드 순번 가져오기
    /// 
    ///  # 스프레드 정보 ####
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_246A2488 : 저장 작업을 실패했습니다.
    ///  TY_M_GB_23NAD871 : 저장하시겠습니까?
    ///  TY_M_GB_23NAD873 : 저장하였습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  SAV : 저장
    ///  Z105035 : 처리구분
    ///  Z105037 : 구매방법
    ///  Z105068 : 거래처
    ///  Z105000 : 대분류코드
    ///  Z105001 : 중분류코드
    ///  Z105002 : 소분류코드
    ///  Z105003 : 품목순번
    ///  Z105004 : HS-CODE
    ///  Z105013 : 자재명１
    ///  Z105015 : 자재명２
    ///  Z105023 : 단위
    ///  Z105025 : 포장단위
    ///  Z105029 : 규격1
    ///  Z105030 : 규격2
    ///  Z105038 : 용도구분
    ///  Z105039 : 구매소요일
    ///  Z105049 : 제작회사
    ///  Z105057 : 최종구매일
    ///  Z105059 : 최종구매단가
    ///  Z105061 : 최종출고일
    ///  Z105065 : 최소재고
    ///  Z105067 : 최대재고
    ///  Z105998 : 비품 (Y)
    /// </summary>
    public partial class TYMRCO007I : TYBase
    {
        private string fsFIJPCODE;
        private string fsFISEQ;

        private TYData DAT30_FIJPCODE1;
        private TYData DAT30_FIJPCODE2;
        //private TYData DAT30_FISEQ;
        private TYData DAT30_FINAME;
        private TYData DAT30_FISIZE;
        private TYData DAT30_FIPHOTO;

        #region Description : 페이지 로드
        public TYMRCO007I(string sFIJPCODE)
        {
            InitializeComponent();

            this.SetPopupStyle();

            // 파라미터값 가져오기
            this.fsFIJPCODE = sFIJPCODE;
        }

        private void TYMRCO007I_Load(object sender, System.EventArgs e)
        {
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            this.TXT01_ATTACH_FILENAME.SetReadOnly(true);
            this.TXT01_FINAME.SetReadOnly(true);
            this.TXT01_FISEQ.SetReadOnly(true);

            UP_Screen_Select("Load");

            SetStartingFocus(this.TXT01_ATTACH_FILENAME);
        }
        #endregion

        #region Description : 저장 버튼
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            string sCOUNT = string.Empty;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_MR_3422H413", this.ControlFactory, "30");

            this.DbConnector.ExecuteTranQuery();

            // 사진 개수 업데이트
            UP_Upt_Photo();

            this.ShowMessage("TY_M_GB_23NAD873");

            UP_Screen_Select("Save");

            // 이미지 화면에 DISPLAY
            //UP_Screen_Display(fsFISEQ.ToString());

            this.TXT01_ATTACH_FILENAME.SetValue("");
            this.TXT01_FINAME.SetValue("");
        }
        #endregion

        #region Description : 삭제 버튼
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_MR_3422I414", fsFIJPCODE.ToString(), fsFISEQ.ToString());

            this.DbConnector.ExecuteNonQuery();

            // 사진 개수 업데이트
            UP_Upt_Photo();

            this.ShowMessage("TY_M_GB_23NAD874");

            UP_Screen_Select("Del");

            // 이미지 화면에 DISPLAY
            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
               (
               "TY_P_MR_3434T423",
               fsFIJPCODE.ToString(),
               fsFISEQ.ToString()
               );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                fsFISEQ = dt.Rows[0]["FISEQ"].ToString();

                this.TXT01_FISEQ.SetValue(dt.Rows[0]["FISEQ"].ToString());

                UP_Image_Display(dt);
            }
            else
            {
                //this.PBX01_IMG.SetValue("");
            }

            this.TXT01_ATTACH_FILENAME.SetValue("");
            this.TXT01_FINAME.SetValue("");
        }
        #endregion

        #region Description : 이전 버튼
        private void BTN61_PRE_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
               (
               "TY_P_MR_3434T423",
               fsFIJPCODE.ToString(),
               fsFISEQ.ToString()
               );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.TXT01_FISEQ.SetValue(dt.Rows[0]["FISEQ"].ToString());

                fsFISEQ = dt.Rows[0]["FISEQ"].ToString();

                UP_Image_Display(dt);
            }
            else
            {
                this.ShowMessage("TY_M_MR_3434V425");
            }
        }
        #endregion

        #region Description : 다음 버튼
        private void BTN61_NEXT_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
               (
               "TY_P_MR_3434V424",
               fsFIJPCODE.ToString(),
               fsFISEQ.ToString()
               );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.TXT01_FISEQ.SetValue(dt.Rows[0]["FISEQ"].ToString());

                fsFISEQ = dt.Rows[0]["FISEQ"].ToString();

                UP_Image_Display(dt);
            }
            else
            {
                this.ShowMessage("TY_M_MR_3434W426");
            }
        }
        #endregion

        #region Description : 닫기 버튼
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;

            this.Close();
        }
        #endregion

        #region Descrption : 조회
        private void UP_Screen_Select(string sGUBUN)
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
               (
               "TY_P_MR_342BE404",
               fsFIJPCODE.ToString()
               );

            this.FPS91_TY_S_MR_342BE406.SetValue(this.DbConnector.ExecuteDataTable());

            if (this.FPS91_TY_S_MR_342BE406.ActiveSheet.RowCount > 0)
            {
                if (sGUBUN.ToString() == "Load")
                {
                    this.TXT01_FISEQ.SetValue(this.FPS91_TY_S_MR_342BE406.GetValue(0, "FISEQ").ToString());

                    fsFISEQ = this.FPS91_TY_S_MR_342BE406.GetValue(0, "FISEQ").ToString();

                    UP_Screen_Display(this.FPS91_TY_S_MR_342BE406.GetValue(0, "FISEQ").ToString());
                }
            }
        }
        #endregion

        #region Descrption : 확인
        private void UP_Screen_Display(string sFISEQ)
        {
            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
               (
               "TY_P_MR_342BY407",
               fsFIJPCODE.ToString(),
               sFISEQ.ToString()
               );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                UP_Image_Display(dt);
            }
        }
        #endregion

        #region Description : 사진 개수 업데이트
        private void UP_Upt_Photo()
        {
            string sCOUNT = string.Empty;

            DataTable dt = new DataTable();

            // 사진 개수 가져오기
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_MR_34228410",
                fsFIJPCODE.ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                sCOUNT = dt.Rows[0]["COUNT"].ToString();
            }

            // 제품 DB 사진개수 업데이트
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_MR_3448Y431", sCOUNT, fsFIJPCODE.ToString());

            this.DbConnector.ExecuteNonQuery();
        }
        #endregion


        #region Description : 이미지 디스플레이
        private void UP_Image_Display(DataTable dt)
        {
            FileStream stream = null;
            byte[] _AttachFile = null;

            try
            {
                string fileName = "c:\\" + dt.Rows[0]["FINAME"].ToString();

                _AttachFile = dt.Rows[0]["FIPHOTO"] as byte[];

                int ArraySize = _AttachFile.GetUpperBound(0);

                PBX01_IMG.SetValue(_AttachFile);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (stream != null)
                    stream.Close();
            }
        }
        #endregion

        #region Description : 저장 체크
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt = new DataTable();

            // 사진 개수 가져오기
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_MR_34228410",
                fsFIJPCODE.ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                if (int.Parse(dt.Rows[0]["COUNT"].ToString()) >= 9)
                {
                    this.ShowMessage("TY_M_MR_3421W408");

                    e.Successed = false;
                    return;
                }
            }

            byte[] _AttachFile = null;

            object _objAttachFile = null;

            string filePath = this.TXT01_ATTACH_FILENAME.GetValue().ToString();

            _AttachFile = UP_Get_Byte(filePath);

            _objAttachFile = _AttachFile;

            int ArraySize = _AttachFile.GetUpperBound(0);
            // 용량체크
            //if (ArraySize > 5000000)
            if (ArraySize > 1000000)
            {
                this.ShowMessage("TY_M_MR_3421W409");

                e.Successed = false;
                return;
            }

            //// 순번 가져오기
            //this.DbConnector.CommandClear();
            //this.DbConnector.Attach
            //    (
            //    "TY_P_MR_3422E412",
            //    fsFIJPCODE.ToString()
            //    );

            //dt = this.DbConnector.ExecuteDataTable();

            //if (dt.Rows.Count > 0)
            //{
            //    this.TXT01_FISEQ.SetValue(dt.Rows[0]["FISEQ"].ToString());

            //    fsFISEQ = dt.Rows[0]["FISEQ"].ToString();
            //}

            this.ControlFactory.Add(new TData("DAT30_FIJPCODE1", fsFIJPCODE.ToString()));
            this.ControlFactory.Add(new TData("DAT30_FIJPCODE2", fsFIJPCODE.ToString()));
            //this.ControlFactory.Add(new TData("DAT30_FISEQ",    fsFISEQ.ToString()));
            this.ControlFactory.Add(new TData("DAT30_FINAME",   this.TXT01_FINAME.GetValue().ToString()));
            this.ControlFactory.Add(new TData("DAT30_FISIZE",   _AttachFile.Length.ToString()));
            this.ControlFactory.Add(new TData("DAT30_FIPHOTO",  _objAttachFile));

            if (!this.ShowMessage("TY_M_GB_23NAD871"))
            {
                e.Successed = false;
                return;
            }
        }
        #endregion

        #region Description : 삭제 체크
        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            if (!this.ShowMessage("TY_M_GB_23NAD872"))
            {
                e.Successed = false;
                return;
            }
        }
        #endregion

        #region Description : 첨부파일 byte 변환
        public static byte[] UP_Get_Byte(string filePath)
        {
            FileInfo file = new FileInfo(filePath);
            //FileStream stream = new FileStream(file.FullName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

            FileStream stream = new FileStream(file.FullName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite, 2000);

            byte[] rawAssembly = new byte[(int)stream.Length];
            stream.Read(rawAssembly, 0, rawAssembly.Length);
            return rawAssembly; // <= byte[] 임
        }
        #endregion

        #region Description : 찾아보기
        private void BTN61_SEARCH_Click(object sender, EventArgs e)
        {
            this.TXT01_FINAME.SetValue("");

            OpenFile.Filter = "JPG(*.jpg)|*.jpg|JPEG(*.jpeg)|*.jpeg|GIF(*.gif)|*.gif|비트맵 파일(*.bmp)|*.bmp|TIFF(*.tif)|*.tif|All Files (*.*)|*.*";

            if (this.OpenFile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.TXT01_ATTACH_FILENAME.Text = this.OpenFile.FileName;

                this.TXT01_FINAME.SetValue(UP_Set_FileName(this.TXT01_ATTACH_FILENAME.Text));
            }
        }
        #endregion

        #region Descrioption : 파일 이름 가져오기
        protected string UP_Set_FileName(string sStr)
        {
            string sValue = "";
            int i = 0;
            int iPoint = 0;
            for (i = 0; i < sStr.Length; i++)
            {
                if (sStr.Substring(i, 1) == "\\")
                {
                    iPoint = i;
                }
            }

            for (i = iPoint + 1; i < sStr.Length; i++)
            {
                sValue = sValue + sStr.Substring(i, 1);
            }

            return sValue;
        }
        #endregion

        #region Description : 스프레드 더블클릭 이벤트
        private void FPS91_TY_S_MR_342BE406_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            this.TXT01_FISEQ.SetValue(this.FPS91_TY_S_MR_342BE406.GetValue("FISEQ").ToString());

            fsFISEQ = this.FPS91_TY_S_MR_342BE406.GetValue("FISEQ").ToString();

            // 파일이름
            this.TXT01_FINAME.SetValue(this.FPS91_TY_S_MR_342BE406.GetValue("FINAME").ToString());

            // 이미지 화면에 DISPLAY
            UP_Screen_Display(fsFISEQ.ToString());
        }
        #endregion

        //private void BTN61_DWN_Click(object sender, EventArgs e)
        //{
        //    //DataTable dt = new DataTable();

        //    //this.DbConnector.CommandClear();
        //    //this.DbConnector.Attach
        //    //   (
        //    //   "TY_P_MR_342BY407",
        //    //   fsFIJPCODE.ToString(),
        //    //   this.TXT01_FISEQ.GetValue().ToString()
        //    //   );

        //    //dt = this.DbConnector.ExecuteDataTable();

        //    //if (dt.Rows.Count > 0)
        //    //{
        //    //    FileStream stream = null;
        //    //    byte[] _AttachFile = null;

        //    //    try
        //    //    {
        //    //        string fileName = "c:\\" + dt.Rows[0]["FINAME"].ToString();

        //    //        _AttachFile = dt.Rows[0]["FIPHOTO"] as byte[];

        //    //        int ArraySize = _AttachFile.GetUpperBound(0);

        //    //        stream = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write);
        //    //        stream.Write(_AttachFile, 0, ArraySize + 1);

        //    //        PBX01_IMG.SetValue(_AttachFile);

        //    //    }
        //    //    catch (Exception ex)
        //    //    {
        //    //        throw ex;
        //    //    }
        //    //    finally
        //    //    {
        //    //        if (stream != null)
        //    //            stream.Close();
        //    //    }
        //    //}
        //}
    }
}