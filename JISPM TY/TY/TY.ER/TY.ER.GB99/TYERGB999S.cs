using System;
using System.Data;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.GB99
{
    /// <summary>
    /// 스프레드 트리그리드 테스트 프로그램입니다.
    /// 
    /// 작성자 : 김영우
    /// 작성일 : 2012.03.23 15:43
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    /// 
    ///  # 스프레드 정보 ####
    ///  TY23N22880 : 트리그리드
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    /// </summary>
    public partial class TYERGB999S : TYBase
    {
        TYCodeBox CBH10_CBHTEST_CUSTUM;
        TYCodeBox CBH10_B2DPAC;

        public TYERGB999S()
        {
            InitializeComponent();

            //TYItemBox 테스트
            this.CBH10_B2DPAC = new TYCodeBox();
            this.CBH10_B2DPAC.Name = "CBH10_B2DPAC";
            this.CBH10_B2DPAC.DummyValue = "20120913";
            this.PAN10_TEST.AddControl("05", "귀속부서", CBH10_B2DPAC);

            this.CBH10_CBHTEST_CUSTUM = new TYCodeBox();
            this.CBH10_CBHTEST_CUSTUM.Name = "CBH10_CBHTEST_CUSTUM";
            this.CBH10_CBHTEST_CUSTUM.DummyValue = new string[] { "21101006", "120952", "12131213" };
            this.PAN10_TEST.AddControl("02", "테스트2", CBH10_CBHTEST_CUSTUM);

            this.ButtonTabIndexLast = false;
        }

        private void TYERGB999S_Load(object sender, System.EventArgs e)
        {
            this.CBH10_CBHTEST_CUSTUM.SetIPopupHelper(new TYERGB994P());
            
            //세션 설정 테스트
            this.Session.SetValue("key", "value");

            #region 오라클/DB2 OUT 파라미터 테스트

            //오라클 OUT 커서 테스트
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY2425S282", "A", "");
            DataTable source = this.DbConnector.ExecuteDataTable();

            //오라클 OUT 파라미터 테스트 - 오라클 OUT 파라미터는 읽기 힘들 듯(20120403)
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY2435G372", "A", "");
            string rtnValue2 = Convert.ToString(this.DbConnector.ExecuteScalar());

            //DB2 프로시져 OUT 파라미터 테스트
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_2433E348", "101", "");
            string rtnValue = Convert.ToString(this.DbConnector.ExecuteScalar()).PadLeft(3, '0');

            string tmp = source.ToString() + rtnValue2 + rtnValue;

            #endregion

            #region 오라클 DataTable 파라미터 테스트 - DataTable 파라미터 날려 ExcuteDataSet으로 한번에 가져오는 기능 없음, 두가지 연동되는게 아님

            //오라클 DataTable 파라미터 테스트
            //this.DbConnector.CommandClear();
            //this.DbConnector.Attach("TYQ0000001", "TY23L3T804");
            //this.DbConnector.Attach("TYQ0000001", "TY23L3T804");
            //DataSet asdasdsa = this.DbConnector.ExecuteDataSet();

            //DataTable tmpParams = new DataTable();
            //tmpParams.Columns.Add();
            //tmpParams.Rows.Add("TY23L3T804");
            //tmpParams.Rows.Add("TY23L3T804");

            //this.DbConnector.CommandClear();
            //this.DbConnector.Attach("TYQ0000001", tmpParams);
            //asdasdsa = this.DbConnector.ExecuteDataSet();
            //asdasdsa.Tables[0].TableName = "asdasd";

            #endregion

            //TYUserInfo
            string deptCode = TYUserInfo.DeptCode;
            string deptName = TYUserInfo.DeptName;

            //DatePicker 테스트
            this.DTP01_DATETEST.SetValue("");

            //체크콤보박스 테스트
            this.CBO01_CBOTEST_CHK.SetValue("ASD");
            this.CBO02_CBOTEST_CHK.SetValue("ASD");
            rtnValue = this.CBO01_CBOTEST_CHK.GetValue().ToString();
            rtnValue = this.CBO02_CBOTEST_CHK.GetValue().ToString();
            this.CBO02_CBOTEST_CHK.SetValue(new string[] { "1", "2" });
            rtnValue = this.CBO02_CBOTEST_CHK.GetValue().ToString();
            this.CBO02_CBOTEST_CHK.SetValue("1;2");
            rtnValue = this.CBO02_CBOTEST_CHK.GetValue().ToString();

            //트리그리드 테스트
            DataSet ds = new DataSet();
            DataTable dataSource, dt0,dt1,dt2;
            Random random = new Random();

            dataSource = new DataTable("TABLE1");
            dataSource.Columns.Add("KEY");
            dataSource.Columns.Add("PARENTKEY");
            dataSource.Columns.Add("DEPTH");
            dataSource.Columns.Add("VAL");
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    for (int k = 0; k==0 || (j > 0 && k < 4); k++)
            {
                dataSource.Rows.Add(
                    string.Format("KEY_{0}{1}{2}", i, j, k),
                    (k > 0 ? string.Format("KEY_{0}{1}0", i, j) : (j > 0 ? string.Format("KEY_{0}00", i) : string.Empty)),
                    (k > 0 ? 2 : (j > 0 ? 1 : 0)),
                    string.Format("VAL({0}_{1}_{2})", i, j, k));
            }

            dt0 = dataSource.Clone();
            dt0.TableName = "TABLE0";
            foreach (DataRow dr in dataSource.Select("DEPTH = 0"))
                dt0.Rows.Add(dr.ItemArray);
            ds.Tables.Add(dt0);
            
                        dt1 = dataSource.Clone();
            dt1.TableName = "TABLE1";
            foreach (DataRow dr in dataSource.Select("DEPTH = 1"))
                dt1.Rows.Add(dr.ItemArray);
            ds.Tables.Add(dt1);
            
            dt2 = dataSource.Clone();
            dt2.TableName = "TABLE2";
            foreach (DataRow dr in dataSource.Select("DEPTH = 2"))
                dt2.Rows.Add(dr.ItemArray);
            ds.Tables.Add(dt2);

            ds.Relations.Add(
                "RELATION_0_1",
                dt0.Columns["KEY"],
                dt1.Columns["PARENTKEY"]);

            ds.Relations.Add(
                "RELATION_1_2",
                dt1.Columns["KEY"],
                dt2.Columns["PARENTKEY"]);

            this.FPS91_TY23N22880.SetValue(ds);
        }

        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            //코드박스 프로시져 변경 테스트
            //this.CBH01_CBHTEST_YWKIM1.Option["C36"] = "TY_P_GB_24242261";
            //this.CBH01_CBHTEST_YWKIM1.SetIPopupHelper(new TY.ER.GB00.TYERGB003P("TY_P_GB_24242261"));
            //this.CBH01_CBHTEST_YWKIM1.Initialize();

            //TYItemBox 테스트
            //if (this.PAN10_TEST.GetCurCode() == "05")
            //    this.PAN10_TEST.SetCurCode("02");
            //else
            //    this.PAN10_TEST.SetCurCode("05");
            string sOUTMSG = string.Empty;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY46C9N752", "12345123451235123451234", "101", "102", "0287-M", sOUTMSG.ToString());
            sOUTMSG = Convert.ToString(this.DbConnector.ExecuteScalar());

            string dddd = sOUTMSG;

        }
    }
}
